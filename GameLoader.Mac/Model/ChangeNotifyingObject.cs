using Foundation;

namespace GameLoader.Mac.Model
{
    public class ChangeNotifyingObject : NSObject
    {
        protected void Set<T>(ref T target, T value, string name)
        {
            WillChangeValue(name);
            target = value;
            DidChangeValue(name);
        }
    }
}
