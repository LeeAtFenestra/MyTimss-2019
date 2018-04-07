
''' <summary>
'''  These interfaces are uses as the contract interface between web parts in
''' various locations in the Reference Application
''' </summary>

Public Interface IShipperInfo
    'OrderEntry.aspx
    ReadOnly Property Text() As String
End Interface
Public Interface IOrdersInfo
    'OrderEntry.aspx and Orders/LookUpMaintenance.aspx
    Property SearchSQL() As String
End Interface
Public Interface IEmployees
    'Employees/LookUpMaintenance.aspx 
    ReadOnly Property QuickSearchType() As String
End Interface
Public Interface IProfileConnection
    'ProfileUpdate.aspx 
    Property UserID() As String
End Interface
