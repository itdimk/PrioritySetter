namespace PrioritySetter.BusinessLogic
{
    public interface ICpuPrioritySetter
    {
    
        public void SetPriority(string appName, CpuPriority cpuPriority);
        public CpuPriority GetPriority(string appName);
    }
}