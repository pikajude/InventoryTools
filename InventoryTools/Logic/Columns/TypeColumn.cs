﻿using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CriticalCommonLib.Models;
using ImGuiNET;
using InventoryTools.Extensions;

namespace InventoryTools.Logic
{
    public class TypeColumn : IColumn
    {
        public string Name { get; set; } = "Type";
        public float Width { get; set; } = 80.0f;
        public string FilterText { get; set; } = "";
        
        public IEnumerable<InventoryItem> Filter(IEnumerable<InventoryItem> items)
        {
            return FilterText == "" ? items : items.Where(c => c.FormattedType.ToLower().PassesFilter(FilterText.ToLower()));
        }

        public IEnumerable<SortingResult> Filter(IEnumerable<SortingResult> items)
        {
            return FilterText == "" ? items : items.Where(c => c.InventoryItem.FormattedType.ToLower().PassesFilter(FilterText.ToLower()));
        }

        public IEnumerable<InventoryItem> Sort(ImGuiSortDirection direction, IEnumerable<InventoryItem> items)
        {
            return direction == ImGuiSortDirection.Ascending ? items.OrderBy(c => c.FormattedType.ToLower()) : items.OrderByDescending(c => c.FormattedType.ToLower());
        }

        public IEnumerable<SortingResult> Sort(ImGuiSortDirection direction, IEnumerable<SortingResult> items)
        {
            return direction == ImGuiSortDirection.Ascending ? items.OrderBy(c => c.InventoryItem.FormattedType.ToLower()) : items.OrderByDescending(c => c.InventoryItem.FormattedType.ToLower());
        }

        public void Draw(InventoryItem item)
        {
            ImGui.TableNextColumn();
            ImGui.Text(item.FormattedType);
        }

        public void Draw(SortingResult item)
        {
            ImGui.TableNextColumn();
            ImGui.Text(item.InventoryItem.FormattedType);
        }

        public void Setup(int columnIndex)
        {
            ImGui.TableSetupColumn(Name, ImGuiTableColumnFlags.WidthFixed, Width,(uint)columnIndex);
        }
    }
}