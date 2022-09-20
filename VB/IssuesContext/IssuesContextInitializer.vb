Imports System
Imports System.Data.Entity
Imports System.Linq

Namespace InfiniteAsyncSourceSkipTokenEFSample

    Public Class IssuesContextInitializer
        Inherits DropCreateDatabaseIfModelChanges(Of IssuesContext)

        ': DropCreateDatabaseAlways<IssuesContext> { 
        Protected Overrides Sub Seed(ByVal context As IssuesContext)
            MyBase.Seed(context)
            Dim users = OutlookDataGenerator.Users.[Select](Function(x)
                Dim split = x.Split(" "c)
                Return New User() With {.FirstName = split(0), .LastName = split(1)}
            End Function).ToArray()
            context.Users.AddRange(users)
            context.SaveChanges()
            Dim rnd = New Random()
            Dim issues = Enumerable.Range(0, 1000).[Select](Function(i) New Issue() With {.Subject = GetSubject(), .UserId = users(rnd.Next(users.Length)).Id, .Created = Date.Today.AddDays(-rnd.Next(30)), .Priority = GetPriority(), .Votes = rnd.Next(100)}).ToArray()
            context.Issues.AddRange(issues)
            context.SaveChanges()
        End Sub
    End Class
End Namespace
