Public Class TIMSSBLLService

    'Daniel Barnhouse, Wei-Tung Liao, 10/5/2017
    'This code is used For the Unit Tests For TIMSS
    'Any BLL change must go to TIMSSBLL.vb and cross referenced. 


    Public Shared Function CalcDaysTillPasswordExpires(CreateDate As DateTime, DaysPasswordIsValid As Integer) As Integer
        Dim ExpirationDate As DateTime = CreateDate.AddDays(DaysPasswordIsValid)
        Dim timeremaining As TimeSpan = ExpirationDate.Subtract(DateTime.Now)
        Return timeremaining.Days
    End Function


End Class
