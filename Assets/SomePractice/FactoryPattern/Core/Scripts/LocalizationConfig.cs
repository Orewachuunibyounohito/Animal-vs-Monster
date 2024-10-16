using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationConfig
{
    public static Dictionary<LocalizedEntry, string> StringTemplates = new Dictionary<LocalizedEntry, string>();
    
    public Dictionary<LocalizedEntry, string> Entries = new Dictionary<LocalizedEntry, string>();

    private LocalizedStringTable _table;

    public LocalizationConfig(){
        _table = new LocalizedStringTable("Hamburger");
        
        EntriesInit();
        StringTemplatesInit();
        foreach(var entry in Enum.GetValues(typeof(LocalizedEntry)).Cast<LocalizedEntry>()){
            EntryBinding(entry);
        }
    }

    private void EntriesInit(){
        Entries.Add(LocalizedEntry.Info, "Template/Info");
    }

    private void StringTemplatesInit(){
        foreach(var entry in Enum.GetValues(typeof(LocalizedEntry)).Cast<LocalizedEntry>()){
            StringTemplates.Add(entry, _table.GetTable().GetEntry(Entries[entry]).GetLocalizedString());
        }
    }

    private void EntryBinding(LocalizedEntry entry){
        LocalizationSettings.SelectedLocaleChanged += (locale) => {
            StringTemplates[entry] = _table.GetTable().GetEntry(Entries[entry]).GetLocalizedString();
        };
    }
}

public enum LocalizedEntry
{
    Info
}