using System;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Components;
using Dalamud.Interface.Utility.Raii;

namespace ARControl.Windows.Config;

internal sealed class MiscTab : ITab
{
    private readonly ConfigWindow _configWindow;
    private readonly Configuration _configuration;

    public MiscTab(ConfigWindow configWindow, Configuration configuration)
    {
        _configWindow = configWindow;
        _configuration = configuration;
    }

    public void Draw()
    {
        using var tab = ImRaii.TabItem(L.Text(LKey.MiscTab));
        if (!tab)
            return;

        ImGui.Text(L.Text(LKey.VentureSettings));
        ImGui.Spacing();

        ImGui.SetNextItemWidth(130);
        int venturesToKeep = _configuration.Misc.VenturesToKeep;
        if (ImGui.InputInt(L.Text(LKey.MinimumVentures), ref venturesToKeep))
        {
            _configuration.Misc.VenturesToKeep = Math.Max(0, Math.Min(65000, venturesToKeep));
            _configWindow.ShouldSave();
        }

        ImGui.SameLine();
        ImGuiComponents.HelpMarker(L.Text(LKey.MinimumVenturesHelp, venturesToKeep));

        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();

        ImGui.Text(L.Text(LKey.UiSettings));

        bool showAssignmentChatMessages = _configuration.ConfigUiOptions.ShowAssignmentChatMessages;
        if (ImGui.Checkbox(L.Text(LKey.ShowAssignmentChat),
                ref showAssignmentChatMessages))
        {
            _configuration.ConfigUiOptions.ShowAssignmentChatMessages = showAssignmentChatMessages;
            _configWindow.ShouldSave();
        }

        bool showContents = _configuration.ConfigUiOptions.ShowVentureListContents;
        if (ImGui.Checkbox(L.Text(LKey.ShowListPreview), ref showContents))
        {
            _configuration.ConfigUiOptions.ShowVentureListContents = showContents;
            _configWindow.ShouldSave();
        }
    }
}
