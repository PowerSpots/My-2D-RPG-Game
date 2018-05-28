using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class NavigationManager
{
    // 游戏中可能的目的地的静态列表
    public static Dictionary<string, Route> RouteInformation = new Dictionary<string, Route>()
    {
        {
            "Overworld",
            new Route { RouteDescription = "The big bad world", CanTravel = true }
        },
        {
            "Construction",
            new Route { RouteDescription = "The construction area", CanTravel = false }
        },
        {
            "Campsite",
            new Route {RouteDescription = "The campsite", CanTravel = false}
        },
        {
            "Town",
            new Route { RouteDescription = "The mian town", CanTravel = true }
        },
        {
            "Shop",
            new Route{RouteDescription="The town shop", CanTravel = true}
        },
    };

    // 获得目的地列表的描述
    public static string GetRouteInfo(string destination)
    {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
    }

    // 是否可以导航到目的地
    public static bool CanNavigate(string destination)
    {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].CanTravel : false;
    }

    // 导航到新场景
    public static void NavigateTo(string destination)
    {
        SceneManager.LoadScene(destination);
    }

    
    public struct Route
    {
        public string RouteDescription;
        public bool CanTravel;
    }
} 