using static System.Console;
using System.Timers;
using System.IO;
//基本思想:
//实例化订阅者的时候,将发布者实例作为参数传递给订阅者构造方法,让发布者
//的事件(委托)列表添加订阅者实例.
//最后发布者手动触发"与触发事件相关的"方法
public class SendMail_eventargs : EventArgs
    {
    public string? Name { get; set; }
    public string? Context { get; set; }
    public string? Time { get; set; }
    }
public class Publisher
    {
    public event EventHandler<SendMail_eventargs> SendMail_event;
    public void SendMail(string name,string context,string time)//设置事件参数，传递给参数对象
        {           //传递参数，激活事件
        SendMail_eventargs mail = new()
            {
            Name = name,
            Context = context,
            Time = time
            };
        SendMail_event?.Invoke(SendMail_event, mail);//参数对象传递给事件
        }
    }
public class Subscriber
    {
    public   Subscriber(Publisher publish)
        {
        publish.SendMail_event += Receivedmail;
        }
    public void Receivedmail(object source, SendMail_eventargs args)//事件传递给处理程序
        {       //接收事件，处理事件
        WriteLine(args.Name );
        WriteLine(args.Context );
        WriteLine(args.Time);
        }
    }
class Program
    {
    public static void Main()
        {
        Publisher tom = new();
        Subscriber jerry = new(tom);
        tom.SendMail("jerry","hello",DateTime.Now.ToString());
        WriteLine("completed");
        }
    }