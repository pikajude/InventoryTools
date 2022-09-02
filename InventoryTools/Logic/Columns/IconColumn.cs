using System.Numerics;
using CriticalCommonLib.Models;
using CriticalCommonLib.Sheets;
using ImGuiNET;
using InventoryTools.Logic.Columns.Abstract;

namespace InventoryTools.Logic.Columns
{
    public class IconColumn : GameIconColumn
    {
        public override (ushort, bool)? CurrentValue(InventoryItem item)
        {
            return (item.Icon, item.IsHQ);
        }

        public override (ushort, bool)? CurrentValue(ItemEx item)
        {
            return (item.Icon, false);
        }

        public override (ushort, bool)? CurrentValue(SortingResult item)
        {
            return CurrentValue(item.InventoryItem);
        }

        public override IColumnEvent? DoDraw((ushort, bool)? currentValue, int rowIndex)
        {
            ImGui.TableNextColumn();
            if (currentValue != null)
            {
                var textureWrap = PluginService.PluginLogic.GetIcon(currentValue.Value.Item1, currentValue.Value.Item2);
                if (textureWrap != null)
                {
                    ImGui.PushID("icon" + rowIndex);
                    if (ImGui.ImageButton(textureWrap.ImGuiHandle, IconSize * ImGui.GetIO().FontGlobalScale,new Vector2(0,0), new Vector2(1,1), 2))
                    {
                        ImGui.PopID();
                        return new ItemIconPressedColumnEvent();
                    }
                    ImGui.PopID();
                }
                else
                {
                    ImGui.Button("", IconSize);
                }
            }
            return null;
            
        }


        public override string Name { get; set; } = "Icon";
        public override float Width { get; set; } = 40.0f;
        public override string HelpText { get; set; } = "Shows the icon of the item, pressing it will open the more information window for the item.";
        public override string FilterText { get; set; } = "";
        public override bool HasFilter { get; set; } = false;
        public override ColumnFilterType FilterType { get; set; } = ColumnFilterType.Text;
    }
}