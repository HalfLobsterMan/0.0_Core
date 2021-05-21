#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 
 *
 */
#endregion
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

namespace CZToolKit.Core.Editors
{
    public class CZTreeViewItem : TreeViewItem
    {
        public object userData;
    }

    public class CZTreeView : TreeView
    {
        int itemCount = 0;

        protected List<TreeViewItem> items = new List<TreeViewItem>();
        protected Dictionary<int, CZTreeViewItem> treeViewItemMap = new Dictionary<int, CZTreeViewItem>();

        public CZTreeView(TreeViewState state) : base(state) { }

        public CZTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader) : base(state, multiColumnHeader) { }

        public bool ShowBoder { get => showBorder; set => showBorder = value; }
        public bool ShowAlternatingRowBackgrounds  { get => showAlternatingRowBackgrounds; set => showAlternatingRowBackgrounds = value; }

        protected override TreeViewItem BuildRoot()
        {
            TreeViewItem root = new TreeViewItem(-1, -1, "Root");
            root.children = items;

            SetupDepthsFromParentsAndChildren(root);
            return root;
        }

        public void AddMenuItem<T>(string _path, T _treeViewItem) where T : CZTreeViewItem
        {
            if (string.IsNullOrEmpty(_path)) return;

            List<TreeViewItem> current = items;
            string[] path = _path.Split('/');
            if (path.Length > 1)
            {
                for (int i = 0; i < path.Length - 1; i++)
                {
                    CZMenuTreeViewItem currentParent = current.Find(t => t.displayName == path[i]) as CZMenuTreeViewItem;
                    if (currentParent == null)
                    {
                        currentParent = new CZMenuTreeViewItem();
                        currentParent.children = new List<TreeViewItem>();
                        currentParent.displayName = path[i];
                        currentParent.id = itemCount;
                        current.Add(currentParent);
                        treeViewItemMap[itemCount] = currentParent;
                        itemCount++;
                    }
                    current = currentParent.children;
                }
            }

            _treeViewItem.id = itemCount;
            _treeViewItem.displayName = path[path.Length - 1];
            _treeViewItem.children = new List<TreeViewItem>();

            current.Add(_treeViewItem);
            treeViewItemMap[itemCount] = _treeViewItem;
            itemCount++;
        }

        public void Remove(CZTreeViewItem _treeViewItem)
        {
            items.Remove(_treeViewItem as TreeViewItem);
            treeViewItemMap.Remove(_treeViewItem.id);
        }

        public CZTreeViewItem Find(int id)
        {
            CZTreeViewItem item = null;
            treeViewItemMap.TryGetValue(id, out item);
            return item;
        }

        public void Clear()
        {
            items.Clear();
            treeViewItemMap.Clear();
        }
    }
}
