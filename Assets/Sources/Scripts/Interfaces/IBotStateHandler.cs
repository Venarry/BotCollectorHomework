public interface IBotStateHandler : IStateSwitcher
{
    public void GoToResource(ITarget target);

    public void GoToFlag(ITarget target);

    public void ChangeBase(ITarget botBase, BaseStorageView baseStorageView);
}
