using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using PVE.Items;
using PVE.Items.Equipment;
using PVE.Items.Weapons;

namespace PVE;

public interface ISaveable
{
    private static readonly string _topLevePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string _gameName = "Holdout";
    private static readonly string _saveFolder = "Saves";
    private static readonly string _dataFolder= "Data";
    
    public static string GetSavePath => Path.Combine(_topLevePath, _gameName, _saveFolder);
    public static string GetDataPath => Path.Combine(_topLevePath, _gameName, _dataFolder);
    
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