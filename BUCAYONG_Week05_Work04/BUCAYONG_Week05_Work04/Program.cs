namespace BUCAYONG_Week05_Work04;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new ProductForm());
    }
}
