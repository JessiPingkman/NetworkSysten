using UnityEngine;

[CreateAssetMenu]
public class NetworgAPISSettings : ScriptableObjectSingleton<NetworgAPISSettings>
{
    public static string AuthorizationToken => Instance._authorizationToken;
    [SerializeField] private string _authorizationToken;
    public static string RefreshToken => Instance._refreshToken;
    [SerializeField] private string _refreshToken;
}