using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using PVE.Items;
using PVE.Items.Equipment;
using PVE.Items.Weapons;

namespace PVE;

public interface ISavable
{
    #if DEBUG
    private static readonly string TopLevePath = @"C:\Development\Projects\C#";
    #else
    private static readonly string TopLevePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    #endif
    
    private static readonly string GameName = "Holdout";
    private static readonly string SaveFolder = "Saves";
    private static readonly string DataFolder= "Data";
    
    public static string GetSavePath => Path.Combine(TopLevePath, GameName, SaveFolder);
    public static string GetDataPath => Path.Combine(TopLevePath, GameName, DataFolder);
    
    public static JsonSerializerOptions GetJsonOptions =>  new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }, 
        TypeInfoResolver = new DefaultJsonTypeInfoResolver
        { Modifiers = 
            {
                typeInfo =>
                {
                    if(typeInfo.Type != typeof(Item)) return;
                    typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                    {
                        TypeDiscriminatorPropertyName = "$type",
                        DerivedTypes =
                        {
                            new JsonDerivedType(typeof(Weapon), "weapon"),
                            new JsonDerivedType(typeof(Armor), "armor")
                        }
                    };
                } 
            }
                    
        }
    };
    public void Serialize();
}