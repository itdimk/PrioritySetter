namespace PrioritySetter.BusinessLogic
{
    public interface ICpuPrioritySetter
    {
        enum CpuPriority
        {
            Idle = 1,
            Normal = 2,
            High = 3,
            RealTime = 4,
            BelowNormal = 5,
            AboveNormal = 6
        }

        public bool SetPriority(string appName, CpuPriority cpuPriority);
        public CpuPriority GetPriority(string appName);
    }
}