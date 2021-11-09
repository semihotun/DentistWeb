using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Handlers.Logs.Models
{
    public class MassageTemplete
    {
        public string ExceptionMessage { get; set; }
        public int FullName { get; set; }
        public int MethodName { get; set; }
        public int Handle { get; set; }
        public int User { get; set; }
        public List<Parameter> Parameters { get; set; }
    }
    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}




// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
//public class Handle
//{
//    public int value { get; set; }
//}

//public class SafeWaitHandle
//{
//    public bool IsInvalid { get; set; }
//    public bool IsClosed { get; set; }
//}

//public class WaitHandle
//{
//    public Handle Handle { get; set; }
//    public SafeWaitHandle SafeWaitHandle { get; set; }
//}

//public class Value
//{
//    public string Lang { get; set; }
//    public bool? IsCancellationRequested { get; set; }
//    public bool? CanBeCanceled { get; set; }
//    public WaitHandle WaitHandle { get; set; }
//}

//public class Parameter
//{
//    public string Name { get; set; }
//    public Value Value { get; set; }
//    public string Type { get; set; }
//}

//public class Root
//{
//    public object FullName { get; set; }
//    public string MethodName { get; set; }
//    public string User { get; set; }
//    public List<Parameter> Parameters { get; set; }
//}

