Imports DevExpress.Data.Filtering
Imports DevExpress.Xpf.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows

Namespace InfiniteAsyncSourceSkipTokenEFSample

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim source = New InfiniteAsyncSource() With {.ElementType = GetType(IssueData)}
            AddHandler Unloaded, Sub(o, e) source.Dispose()
            AddHandler source.FetchRows, Sub(o, e)
                e.Result = Task.Run(Function() FetchRows(e))
            End Sub
            Me.grid.ItemsSource = source
        End Sub

        Private Shared Function FetchRows(ByVal e As FetchRowsAsyncEventArgs) As FetchRowsResult
            Dim filterWithSkipToken = CriteriaOperator.And(e.Filter, CType(e.SkipToken, CriteriaOperator))
            Dim converter = New GridFilterCriteriaToExpressionConverter(Of IssueData)()
            Dim filterExpression = converter.Convert(filterWithSkipToken)
            Const defaultUniqueSortProperty As String = "Id"
            Dim context = New IssuesContext()
            Dim queryable = IssueData.Select(context.Issues).SortBy(e.SortOrder, defaultUniqueSortPropertyName:=defaultUniqueSortProperty).Where(filterExpression)
            Dim issues = queryable.Take(30).ToArray()
            Dim nextSkipToken = SkipTokenHelper.MakeFilterSkipToken(e.SortOrder, defaultUniqueSortProperty, issues.LastOrDefault())
            Return New FetchRowsResult(issues, nextSkipToken:=nextSkipToken)
        End Function
    End Class
End Namespace
