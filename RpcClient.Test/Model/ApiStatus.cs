using System.ComponentModel;

namespace RpcClient.Test.Model
{
    public enum ApiStatus
    {
        [Description("成功")]
        Success = 200,
        [Description("身份验证失败")]
        UnAuthorized = 403,
        [Description("失败")]
        Fail = 500
    }
}
