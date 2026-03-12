// src/SAValidation.PhoneNumbers/AreaCodeDatabase.cs
using System.Collections.Generic;

namespace SAValidation.PhoneNumbers;

/// <summary>
/// Database of South African area codes and their descriptions
/// </summary>
internal static class AreaCodeDatabase
{
    /// <summary>
    /// Dictionary of area codes and their descriptions
    /// </summary>
    public static readonly IReadOnlyDictionary<string, string> AreaCodes = new Dictionary<string, string>
    {
        // Major cities
        ["011"] = "Johannesburg & Gauteng",
        ["012"] = "Pretoria & Tshwane",
        ["013"] = "Mpumalanga & Nelspruit",
        ["014"] = "Northern Province & Rustenburg",
        ["015"] = "Polokwane & Limpopo",
        ["016"] = "Vaal Triangle & Vanderbijlpark",
        ["017"] = "Ermelo & Mpumalanga South",
        ["018"] = "Potchefstroom & Klerksdorp",
        ["021"] = "Cape Town & Winelands",
        ["023"] = "Karoo & Beaufort West",
        ["027"] = "Northern Cape & Vioolsdrift",
        ["028"] = "Southern Cape & Hermanus",
        ["031"] = "Durban & eThekwini",
        ["033"] = "Pietermaritzburg & Midlands",
        ["034"] = "Newcastle & Northern KZN",
        ["035"] = "Richards Bay & Zululand",
        ["036"] = "Ladysmith & Drakensberg",
        ["039"] = "Port Shepstone & South Coast",
        ["041"] = "Port Elizabeth & Gqeberha",
        ["042"] = "Jeffreys Bay & Tsitsikamma",
        ["043"] = "East London & Buffalo City",
        ["044"] = "Garden Route & Knysna",
        ["045"] = "Queenstown & Eastern Cape",
        ["046"] = "Grahamstown & Makhanda",
        ["047"] = "Mthatha & Wild Coast",
        ["048"] = "Namaqualand & Springbok",
        ["049"] = "Graaff-Reinet & Midlands",
        ["051"] = "Bloemfontein & Free State",
        ["053"] = "Kimberley & Northern Cape",
        ["054"] = "Upington & Kalahari",
        ["056"] = "Kroonstad & Northern Free State",
        ["057"] = "Welkom & Goldfields",
        ["058"] = "Bethlehem & Eastern Free State"
    };
    
    /// <summary>
    /// Gets the description for an area code
    /// </summary>
    public static string? GetDescription(string areaCode)
    {
        return AreaCodes.TryGetValue(areaCode, out var description) ? description : null;
    }
    
    /// <summary>
    /// Checks if an area code is valid
    /// </summary>
    public static bool IsValidAreaCode(string areaCode)
    {
        return AreaCodes.ContainsKey(areaCode);
    }
}