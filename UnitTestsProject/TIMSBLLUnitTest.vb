Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Westat.TIMSS.BLL
<TestClass()> Public Class CalDaysTillPasswordExpiresTest

    <TestMethod()> Public Sub ShouldReturnZerodaysCalcDaysTillPasswordExpires()
        Dim createdate As DateTime = Today.AddDays(-120)
        Assert.AreEqual(0, TIMSSBLLService.CalcDaysTillPasswordExpires(createdate, 120))
        'Assert.AreEqual("hello", "hello")

    End Sub

    <TestMethod()> Public Sub ShouldReturn10daysCalcDaysTillPasswordExpires()
        Dim createdate As DateTime = Today.AddDays(-109)
        Assert.AreEqual(10, TIMSSBLLService.CalcDaysTillPasswordExpires(createdate, 120))
        'Assert.AreEqual("hello", "hello")

    End Sub
End Class