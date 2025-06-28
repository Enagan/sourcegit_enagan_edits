using System.Collections.Generic;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Threading;

namespace SourceGit.Views
{
    public partial class CommitChanges : UserControl
    {
        public CommitChanges()
        {
            InitializeComponent();
        }

        private void OnChangeContextRequested(object sender, ContextRequestedEventArgs e)
        {
            if (sender is ChangeCollectionView { SelectedChanges: { } selected } view &&
                selected.Count == 1 &&
                DataContext is ViewModels.CommitDetail vm)
            {
                var menu = vm.CreateChangeContextMenu(selected[0]);
                menu?.Open(view);
            }

            e.Handled = true;
        }
    }
}
