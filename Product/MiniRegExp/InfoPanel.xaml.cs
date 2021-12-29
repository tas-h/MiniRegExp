using LiveCharts;
using LiveCharts.Wpf;
using RegExpService;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MiniRegExp
{
    /// <summary>
    /// Interaction logic for InfoPanel.xaml
    /// </summary>
    public partial class InfoPanel : Window
    {
        public string[] Labels { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<int, string> YFormatter { get; set; }

        private readonly MatchesInfo matchesInfos;
        private static SolidColorBrush matchForeground = new SolidColorBrush(Colors.White);
        private static SolidColorBrush groupForeground = new SolidColorBrush(Colors.Yellow);

        public InfoPanel(MatchesInfo pMatchesInfos)
        {
            InitializeComponent();
            matchesInfos = pMatchesInfos;
            RefreshTreeViewRegex();
            RefreshDataGridRegex();
            RefreshLiveChart();
        }

        private void RefreshDataGridRegex()
        {
            DataGridRegex.ItemsSource = matchesInfos.GetMatchesInfo();
        }

        private void RefreshTreeViewRegex()
        {
            for (int i = 0; i < matchesInfos.Count; i++)
            {
                int i1 = i + 1;
                TreeViewItem t1 = new() { Header = $"{i1}\t{matchesInfos[i].ToString()}", IsExpanded = true, Foreground = matchForeground};
                for (int j = 0; j < matchesInfos[i].GroupCount; j++)
                    t1.Items.Add(new TreeViewItem() { Header = $"{i1}/{j+1}\t{matchesInfos[i][j].ToString()}", IsExpanded = true, Foreground = groupForeground });
                TreeViewRegex.Items.Add(t1);
            }
        }

        private void RefreshLiveChart()
        {
            SeriesCollection = new SeriesCollection();
            //Labels = new string[matchInfos.Length];
            //Labels = Enumerable.Range(1, matchesInfos.InputLength).Select(x => x.ToString()).ToArray();
            MatchGroupedInfo[] matchGroupedInfos = matchesInfos.GetMatchGroupedInfo();
            Labels = Enumerable.Range(0, matchesInfos.Count + 1).Select(x => x.ToString()).ToArray();
            int[][] matchInfoList = new int[matchGroupedInfos.Length][];
            for (int i = 0; i < matchGroupedInfos.Length; i++)
            {
                matchInfoList[i] = new int[matchesInfos.Count + 1];
                matchInfoList[i][0] = 0;
            }

            for (int i = 0; i < matchesInfos.Count; i++)
            {
                for (int j = 0; j < matchGroupedInfos.Length; j++)
                    if (matchGroupedInfos[j].Value == matchesInfos[i].Value)
                        matchInfoList[j][i + 1] = matchInfoList[j][i] + 1;//matchesInfos[i].Index;
                    else
                        matchInfoList[j][i + 1] = matchInfoList[j][i];
            }

            SeriesCollection = new SeriesCollection();
            for (int i = 0; i < matchInfoList.Length; i++)
            {
                SeriesCollection.Add(new LineSeries
                {
                    LineSmoothness = 0,
                    Title = matchGroupedInfos[i].Value.Length > 10 ? $"{i+1}: {matchGroupedInfos[i].Value.Substring(1, 7)}..." : $"{i+1}: {matchGroupedInfos[i].Value}",
                    Values = new ChartValues<int>(matchInfoList[i])
                });
            }
            
            YFormatter = value => value.ToString("N0");
            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.Step = 1;
            AxisY.Separator = sep;
            AxisY.MinValue = 0;
            //SeriesCollection[Labels.Length].Values.Add(5d);

            DataContext = this;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonExpandAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (TreeViewItem treeViewItem in TreeViewRegex.Items)
                treeViewItem.IsExpanded = true;
        }

        private void ButtonCollapsedAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (TreeViewItem treeViewItem in TreeViewRegex.Items)
                treeViewItem.IsExpanded = false;
        }
    }
}
