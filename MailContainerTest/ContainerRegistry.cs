using System;
using System.Reflection;
using MailContainerTest.Data;
using MailContainerTest.DependencyInjection;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Microsoft.Extensions.DependencyInjection;

namespace MailContainerTest
{
	public static class ContainerRegistry
	{
        public static void Initilize()
        {
            IocHelper.RegisterSingleton<IMailTransferService, MailTransferService>();
            IocHelper.RegisterSingleton<IMailContainerDataStore, BackupMailContainerDataStore>();
            IocHelper.RegisterSingleton<IMailContainerDataStore, MailContainerDataStore>();

            RegisterDependencies();
            IocHelper.Build();
        }

        private static void RegisterDependencies()
        {
            var asm = Assembly.Load(new AssemblyName("MailContainerTest"));

            //Register Transient instance of all ContentPages 
            IocHelper.RegisterAssemblyTypes(asm, "Result", typeof(BaseModel), ServiceLifetime.Transient);

            //Register Transient instance of all ViewModels
            //IocHelper.RegisterAssemblyTypes(asm, "ViewModel", typeof(BaseViewModel), ServiceLifetime.Transient);
        }
    }
}