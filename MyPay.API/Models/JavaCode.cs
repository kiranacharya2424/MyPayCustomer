//using System;
//using System.Runtime.InteropServices;

//class JavaCode
//{
//    // Define the JavaVM pointer
//    private static IntPtr jvm;

//    [DllImport("JavaCodeAlgorithm.dll", CharSet = CharSet.Auto)]
//    private static extern int JNI_CreateJavaVM(out IntPtr pvm, out IntPtr penv, IntPtr args);

//    [DllImport("JavaCodeAlgorithm.dll", CharSet = CharSet.Auto)]
//    private static extern int JNI_GetCreatedJavaVMs(out IntPtr pvm, uint size, out uint nsize);

//    [DllImport("JavaCodeAlgorithm.dll", CharSet = CharSet.Auto)]
//    private static extern int JNI_GetDefaultJavaVMInitArgs(ref IntPtr args);

//   public static void Main(string[] args)
//    {
//        // Create the Java Virtual Machine
//        int result = JNI_CreateJavaVM(out jvm, out IntPtr penv, IntPtr.Zero);

//        if (result < 0)
//        {
//            Console.WriteLine("Error creating JVM");
//            return;
//        }

//        //try
//        //{
//        //    // Load the Java class
//        //    IntPtr cls = penv.FindClass("EncryptionInJava");

//        //    if (cls != IntPtr.Zero)
//        //    {
//        //        // Find the method ID for the "greet" method
//        //        IntPtr mid = penv.GetStaticMethodID(cls, "greet", "(Ljava/lang/String;)V");

//        //        if (mid != IntPtr.Zero)
//        //        {
//        //            // Convert the .NET string to a Java string
//        //            IntPtr javaString = penv.NewString("John");

//        //            // Call the Java method
//        //            penv.CallStaticVoidMethod(cls, mid, javaString);
//        //        }
//        //        else
//        //        {
//        //            Console.WriteLine("Method not found");
//        //        }
//        //    }
//        //    else
//        //    {
//        //        Console.WriteLine("Class not found");
//        //    }
//        //}
//        //catch (Exception e)
//        //{
//        //    Console.WriteLine("Exception: " + e.Message);
//        //}
//        //finally
//        //{
//        //    // Destroy the Java Virtual Machine
//        //    JNI_DestroyJavaVM(jvm);
//        //}
//    }
//}



////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Web;

////namespace MyPay.API.Models
////{
////    using System;
////    using System.Runtime.InteropServices;

////   public class javacode
////    {
////        [DllImport("JavaCodeAlgorithm.dll", CharSet = CharSet.Auto)]
////        public static extern string Encryption( string[] args);
////       public static string  Main(string[] args)
////        {
////            IntPtr jvm;
////            IntPtr penv;
////            string result = Encryption( args);
////            return result;  

////            // Destroy the Java Virtual Machine
////            //JNI_DestroyJavaVM(jvm);
////        }
////    }

////}