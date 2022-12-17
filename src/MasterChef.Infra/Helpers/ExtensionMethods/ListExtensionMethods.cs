using System.Collections.Generic;
using System.Linq;

namespace MasterChef.Infra.Helpers.ExtensionMethods;

public static class ListExtensionMethods
{
    public static bool HasItems(this List<string> list) => 
        list != null && list.Any();
}