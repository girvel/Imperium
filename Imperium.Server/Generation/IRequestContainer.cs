namespace Imperium.Server.Generation
{
    public interface IRequestContainer<TGlobalData>
    {
        TGlobalData GlobalData { get; set; }
    }
}