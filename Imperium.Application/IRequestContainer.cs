using Imperium.Game;

namespace Imperium.Application
{
    public interface IRequestContainer
    {
        GameData GameData { get; set; }
    }
}