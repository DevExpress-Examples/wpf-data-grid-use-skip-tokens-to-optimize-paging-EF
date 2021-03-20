using DevExpress.Data.Filtering;
using DevExpress.Xpf.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InfiniteAsyncSourceSkipTokenEFSample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var source = new InfiniteAsyncSource() {
                ElementType = typeof(IssueData)
            };

            Unloaded += (o, e) => {
                source.Dispose();
            };

            source.FetchRows += (o, e) => {
                e.Result = Task.Run(() => FetchRows(e));
            };

            grid.ItemsSource = source;
        }

        static FetchRowsResult FetchRows(FetchRowsAsyncEventArgs e) {
            var filterWithSkipToken = CriteriaOperator.And(e.Filter, (CriteriaOperator)e.SkipToken);

            var converter = new GridFilterCriteriaToExpressionConverter<IssueData>();
            var filterExpression = converter.Convert(filterWithSkipToken);

            const string defaultUniqueSortProperty = "Id";

            var context = new IssuesContext();
            var queryable = IssueData.Select(context.Issues)
                .SortBy(e.SortOrder, defaultUniqueSortPropertyName: defaultUniqueSortProperty)
                .Where(filterExpression);

            var issues = queryable
                .Take(e.Take ?? 30)
                .ToArray();

            var nextSkipToken = SkipTokenHelper.MakeFilterSkipToken(e.SortOrder, defaultUniqueSortProperty, issues.LastOrDefault());

            return new FetchRowsResult(issues, nextSkipToken: nextSkipToken);
        }
    }
}
