using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace WebRole1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Microsoft.ServiceHosting.Tools.DevelopmentFabric.Runtime.DevelopmentFabricTraceListener _tmpListener;

        // Guiao - 2 - Passo Passo 5.b. 1 Adic 'a classe Default.aspx tratm evnt e adic novo txt na queue
        protected void ClicarNoBotao(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            // Guiao - 2 - Passo 5.b. : Passo 4.a. Para criar a queue
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(RoleEnvironment
                                            .GetConfigurationSettingValue("StorageConnectionString"));

            // Guiao - 2 - Passo 5.b. : Passo 4.b. Para criar o cliente da queue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Guiao - 2 - Passo 5.b. : Passo 4.c. Para retornar a referencia da queue
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Guiao - 2 - Passo 5.b. : Passo 4.d. Para criar a queue se ainda nao existir
            queue.CreateIfNotExist();

            CloudQueueMessage m = new CloudQueueMessage(TextBoxProcess.Text); // cod nao esta' no guiao
            // Guiao - 2 - Passo 5.b. 2 A adicao e' realizada atraves de
            queue.AddMessage(m);

            // Codigo criado para se ver o envio da mensagem, tanto no trace como na aplicacao web

            //Trace.Write("Sent message:" + m.AsString); // Page.Trace = class System.Web.TraceContext
            //System.Diagnostics.Trace.WriteLine("Sent message:" + m.AsString);
// http://social.msdn.microsoft.com/Forums/en-US/windowsazuretroubleshooting/thread/b116f0ee-ea77-4168-bcd1-59fc48647116
            // http://stackoverflow.com/questions/4496027/trace-writeline-in-asp-net-azure
            // http://blog.elastacloud.com/2011/01/22/tracing-to-azure-compute-emulator-sdk-v1-3/
            // http://blog.elastacloud.com/2011/01/12/updating-azure-diagnostics-to-sdk-v1-3-2/
            // http://www.docstoc.com/docs/87749014/Debugging-Applications-in-Windows-Azure
            if (!RoleEnvironment.IsAvailable)
            {
                //if (_tmpListener == null)
                //{
                //    _tmpListener = new DevelopmentFabricTraceListener();
                //    System.Diagnostics.Trace.Listeners.Add(_tmpListener);
                //}
            }

            LabelListaProcessMsgsSent.Text = LabelListaProcessMsgsSent.Text + "<br />" + m.AsString;
        }
    }
}
