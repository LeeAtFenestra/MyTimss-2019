Imports System.Security.Permissions

Namespace Westat.FAB.SiteMapProvider

    <AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Minimal)> _
    Public Class CustomSiteMapProvider
        Inherits StaticSiteMapProvider

        ''' <summary>
        ''' This class implements a customized SiteMapProvider
        ''' It inherits from the StaticSiteMapProvider, read the attributes of each node from the table of Database, and build the hierarchy of site map nodes.
        ''' The Schema of the table is as following:
        ''' CREATE TABLE [dbo].[siteMap_FAB](
        '''               [NODEID] [int] NOT NULL,
        '''               [URL] [varchar](255)  ,
        '''               [NAME] [varchar](255),
        '''               [PARENTNODEID] [int] NULL,
        '''               [DESCRIPTION] [varchar](500) ,
        '''               [ROLES] [varchar](50) ,
        '''               CONSTRAINT [PK_siteMap_FAB] PRIMARY KEY CLUSTERED 
        '''               (
        '''	                 [NODEID] ASC
        '''               )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
        '''    ) ON [PRIMARY]
        ''' </summary>
        ''' 
        Private _smnRootNode As SiteMapNode = Nothing
        ' This string is case sensitive.
        Private _strSiteMapTableName As String = "siteMapTable"
        Private _strSiteMapTable As String = Nothing
        Private _strSiteMapName As String = "siteMapName"
        Private _strSiteMap As String = Nothing

        ' Some basic state to help track the initialization state of the provider.
        Private _bInitialized As Boolean = False

        ' Implement a default constructor.
        Public Sub New()

        End Sub 'New


        Public Overridable ReadOnly Property IsInitialized() As Boolean
            Get
                Return _bInitialized
            End Get
        End Property

        ' Return the root node of the current site map.
        Public Overrides ReadOnly Property RootNode() As SiteMapNode
            Get
                Return BuildSiteMap()
            End Get
        End Property

        Protected Overrides Function GetRootNodeCore() As SiteMapNode
            Return RootNode
        End Function

        ' Initialize is used to initialize the properties and any state that the
        ' CustomSiteMapProvider holds, but is not used to build the site map.
        ' The site map is built when the BuildSiteMap method is called.
        Public Overrides Sub Initialize(ByVal name As String, ByVal attributes As NameValueCollection)
            If IsInitialized Then
                Return
            End If
            MyBase.Initialize(name, attributes)

            _strSiteMapTable = attributes(_strSiteMapTableName)
            _strSiteMap = attributes(_strSiteMapName)

            If Nothing = _strSiteMapTable OrElse _strSiteMapTable.Length = 0 Then
                Throw New Exception("The SiteMap table was not found.")
            End If
            _bInitialized = True
        End Sub 'Initialize

        Public Sub Refresh()
            Clear()
        End Sub
        ' SiteMapProvider and StaticSiteMapProvider methods that this derived class must override.
        '
        ' Clean up any collections or other state that an instance of this may hold.
        Protected Overrides Sub Clear()
            SyncLock Me
                _smnRootNode = Nothing
                MyBase.Clear()
            End SyncLock
        End Sub 'Clear

        'Build the hierarchy of site map nodes
        Protected Sub BuildSiteMapNodes(ByVal RootNodeID As String, ByVal CurrRootNode As SiteMapNode)
            Dim strRoles As String = Nothing
            Dim smnChildNode As SiteMapNode = Nothing
            Dim strRoleNames As String()

            Dim db As Database = DatabaseFactory.CreateDatabase()
            Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand("FAB_GetSiteMap")
            db.AddInParameter(dbCommand, "SiteMapTable", DbType.String, _strSiteMapTable)
            db.AddInParameter(dbCommand, "ParentNodeID", DbType.Int16, RootNodeID)
            If (_strSiteMap IsNot Nothing) Then
                db.AddInParameter(dbCommand, "siteMapName", DbType.String, _strSiteMap)
            End If

            Using daReader As IDataReader = db.ExecuteReader(dbCommand)
                While daReader.Read()
                    RootNodeID = daReader("NODEID").ToString
                    strRoles = daReader("ROLES").ToString
                    Dim Roles As New System.Collections.ArrayList

                    If Not strRoles = "" Then
                        'If attribute "roles" is specified for the node.
                        strRoleNames = strRoles.Split("|")
                        Dim role As String = String.Empty
                        For Each role In strRoleNames
                            Roles.Add(role.ToString)
                        Next
                        smnChildNode = New SiteMapNode(Me, _
                        RootNodeID, _
                        daReader("URL").ToString, _
                        daReader("NAME").ToString, _
                        daReader("DESCRIPTION").ToString, _
                        Roles, Nothing, Nothing, "")
                    Else
                        smnChildNode = New SiteMapNode(Me, _
                        RootNodeID, _
                        daReader("URL").ToString, _
                        daReader("NAME").ToString, _
                        daReader("DESCRIPTION").ToString)
                    End If
                    AddNode(smnChildNode, CurrRootNode)
                    BuildSiteMapNodes(RootNodeID, smnChildNode)
                End While
                Return
            End Using
        End Sub

        ' Build an in-memory representation from persistent
        ' storage, and return the root node of the site map.
        Public Overrides Function BuildSiteMap() As SiteMapNode
            Dim strRoleNames As String()

            ' Since the SiteMap class is static, make sure that it is
            ' not modified while the site map is built.
            SyncLock Me
                ' If there is no initialization, this method is being
                ' called out of order.
                If Not IsInitialized Then
                    Throw New Exception("BuildSiteMap called incorrectly.")
                End If

                ' If there is no root node, then there is no site map.
                If _smnRootNode Is Nothing Then
                    ' Start with a clean slate
                    Clear()
                    Dim db As Database = DatabaseFactory.CreateDatabase()
                    Dim dbCommand As Common.DbCommand = db.GetStoredProcCommand("FAB_GetSiteMap")
                    db.AddInParameter(dbCommand, "SiteMapTable", DbType.String, _strSiteMapTable)
                    db.AddInParameter(dbCommand, "ParentNodeID", DbType.Int16, Nothing)
                    If (_strSiteMap IsNot Nothing) Then
                        db.AddInParameter(dbCommand, "siteMapName", DbType.String, _strSiteMap)
                    End If

                    Using daReader As IDataReader = db.ExecuteReader(dbCommand)
                        If daReader.Read() Then
                            'Select the root node of the site map from the table.
                            Dim strRootNodeId As String
                            Dim strRoles As String = Nothing
                            Dim Roles As New System.Collections.ArrayList
                            strRoles = daReader("ROLES").ToString
                            strRootNodeId = daReader("NODEID").ToString
                            If Not strRoles = "" Then
                                strRoleNames = strRoles.Split("|")
                                Dim role As String = String.Empty
                                For Each role In strRoleNames
                                    Roles.Add(role.ToString)
                                Next
                                _smnRootNode = New SiteMapNode(Me, _
                                strRootNodeId, _
                                daReader("URL").ToString, _
                                daReader("NAME").ToString, _
                                daReader("DESCRIPTION").ToString, _
                                Roles, Nothing, Nothing, "")
                            Else
                                _smnRootNode = New SiteMapNode(Me, _
                                strRootNodeId, _
                                daReader("URL").ToString, _
                                daReader("NAME").ToString, _
                                daReader("DESCRIPTION").ToString)
                            End If
                            BuildSiteMapNodes(strRootNodeId, _smnRootNode)
                        Else
                            Return Nothing
                        End If
                    End Using
                End If
                Return _smnRootNode
            End SyncLock
        End Function 'BuildSiteMap
        ' The following function can be customized when the role specified for the node changes
        'Public Overrides Function IsAccessibleToUser(ByVal context As System.Web.HttpContext, ByVal node As System.Web.SiteMapNode) As Boolean
        '    Dim bHasAccessRight As Boolean

        '    bHasAccessRight = True
        '    If (node.Roles IsNot Nothing) Then
        '        For Each role As String In node.Roles
        '            If (context.User.IsInRole(role)) Then
        '                Return True
        '            ElseIf (node.Url = context.Request.AppRelativeCurrentExecutionFilePath) Then
        '                bHasAccessRight = False
        '            End If
        '        Next
        '        If (bHasAccessRight = False) Then
        '            context.Response.Redirect("~/login.aspx")
        '        End If
        '        Return False
        '    Else
        '        Return MyBase.IsAccessibleToUser(context, node)
        '    End If
        'End Function 'IsAccessibleToUser

    End Class 'CustomSiteMapProvider

End Namespace
