using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domainVerify
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------domainVerity 1.0----------");
            Console.Write("Please input your AD:");
            string ad_name = Console.ReadLine();
            //Console.Write("and your password:");
            //string ad_pwd = Console.ReadLine();

            string dePath = @"LDAP://10.33.1.8/DC=IFC,DC=local";
        //PAIC:
            using (DirectoryEntry dtest = new DirectoryEntry(dePath))
            {
                DirectorySearcher src = new DirectorySearcher(dtest);
                src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + ad_name + "))";
                src.PropertiesToLoad.Add("cn");
                src.SearchRoot = dtest;
                src.SearchScope = SearchScope.Subtree;
                SearchResult result;
                try
                {
                    result = src.FindOne();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return;
                }
                if (result != null)
                {
                    DirectoryEntry de = result.GetDirectoryEntry();
                    string cn = de.Name;
                    Console.WriteLine("Verity Succeed！ Msg:" + cn.Substring(3));
                }
            }

            //using (DirectoryEntry deUser = new DirectoryEntry(dePath, ad_name, ad_pwd))
            //{
            //    DirectorySearcher src = new DirectorySearcher(deUser);
            //    src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + ad_name + "))";
            //    src.PropertiesToLoad.Add("cn");
            //    src.SearchRoot = deUser;
            //    src.SearchScope = SearchScope.Subtree;
            //    SearchResult result;
            //    try
            //    {
            //        result = src.FindOne();
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Password Error!");
            //        return;
            //    }
            //    if (result != null)
            //    {
            //        DirectoryEntry de = result.GetDirectoryEntry();
            //        string cn = de.Name;
            //        Console.WriteLine("Verity Succeed！ Msg:" + cn.Substring(3));
            //    }
            //    else
            //    {
            //        dePath = @"LDAP://10.33.3.12/DC=IFRDOM,DC=local";
            //        goto PAIC;
            //    }
            //}
        }
    }
}
