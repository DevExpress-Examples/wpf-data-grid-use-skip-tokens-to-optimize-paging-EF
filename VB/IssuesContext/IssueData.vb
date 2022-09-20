Imports System
Imports System.Linq

Namespace InfiniteAsyncSourceSkipTokenEFSample

    Public Class IssueData

        Private _Id As Integer, _Subject As String, _Created As DateTime, _Votes As Integer, _Priority As Priority, _User As String

        Public Shared Function [Select](ByVal issues As System.Linq.IQueryable(Of InfiniteAsyncSourceSkipTokenEFSample.Issue)) As IQueryable(Of InfiniteAsyncSourceSkipTokenEFSample.IssueData)
            Return issues.[Select](Function(x) New InfiniteAsyncSourceSkipTokenEFSample.IssueData() With {.Id = x.Id, .Subject = x.Subject, .User = x.User.FirstName & " " & x.User.LastName, .Created = x.Created, .Votes = x.Votes, .Priority = x.Priority})
        End Function

        Public Property Id As Integer
            Get
                Return _Id
            End Get

            Private Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Public Property Subject As String
            Get
                Return _Subject
            End Get

            Private Set(ByVal value As String)
                _Subject = value
            End Set
        End Property

        Public Property Created As DateTime
            Get
                Return _Created
            End Get

            Private Set(ByVal value As DateTime)
                _Created = value
            End Set
        End Property

        Public Property Votes As Integer
            Get
                Return _Votes
            End Get

            Private Set(ByVal value As Integer)
                _Votes = value
            End Set
        End Property

        Public Property Priority As Priority
            Get
                Return _Priority
            End Get

            Private Set(ByVal value As Priority)
                _Priority = value
            End Set
        End Property

        Public Property User As String
            Get
                Return _User
            End Get

            Private Set(ByVal value As String)
                _User = value
            End Set
        End Property
    End Class
End Namespace
