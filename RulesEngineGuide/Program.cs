using Newtonsoft.Json;
using RulesEngine.Models;
using static RulesEngineGuide.Program.UserInput;

namespace RulesEngineGuide
{
    internal class Program
    {
        #region demo1简单的规则引擎使用方法
        //public class UserInput
        //{
        //    public string IdCard { get; set; }
        //    public int Age { get; set; }
        //}


        //[Obsolete]
        //static async Task Main(string[] args)
        //{
        //    //规则表达式json
        //    var rulesStr = @"[{
        //            ""WorkflowName"": ""UserInputWorkflow"",
        //            ""Rules"": [
        //              {
        //                ""RuleName"": ""CheckAge"",
        //                ""ErrorMessage"": ""年龄必须大于18岁."",
        //                ""ErrorType"": ""Error"",
        //                ""RuleExpressionType"": ""LambdaExpression"",
        //                ""Expression"": ""Age > 18""
        //              },
        //               {
        //                ""RuleName"": ""CheckIdCardIsEmpty"",
        //                ""ErrorMessage"": ""身份证号不可以为空."",
        //                 ""ErrorType"": ""Error"",
        //                ""RuleExpressionType"": ""LambdaExpression"",
        //                ""Expression"": ""IdCard != null""
        //              }
        //            ]
        //          }] ";

        //    //输入的数据
        //    var userInput = new UserInput
        //    {
        //        IdCard = null,
        //        Age = 18,
        //    };


        //    //反序列化Json格式规则字符串
        //    var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(rulesStr);
        //    var rulesEngine = new RulesEngine.RulesEngine(workflowRules!.ToArray());

        //    List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("UserInputWorkflow", userInput);
        //    foreach (var item in resultList)
        //    {
        //        Console.WriteLine("验证成功：{0}，消息：{1}", item.IsSuccess, item.ExceptionMessage);
        //    }

        //    Console.ReadLine();
        //}
        #endregion

        #region demo2表达式内使用扩展方法
        //public class UserInput
        //{
        //    public string IdCard { get; set; }
        //    public int Age { get; set; }
        //}

        ////自定义类型
        //private static readonly ReSettings reSettings = new ReSettings
        //{
        //    CustomTypes = new[] { typeof(IdCardUtil) }
        //};

        //[Obsolete]
        //static async Task Main(string[] args)
        //{
        //    //规则表达式json
        //    var rulesStr = @"[{
        //                ""WorkflowName"": ""UserInputWorkflow"",
        //                ""Rules"": [
        //                  {
        //                    ""RuleName"": ""CheckAge"",
        //                    ""ErrorMessage"": ""年龄必须大于18岁."",
        //                    ""ErrorType"": ""Error"",
        //                    ""RuleExpressionType"": ""LambdaExpression"",
        //                    ""Expression"": ""IdCard.GetAgeByIdCard() > 18""
        //                  },
        //                   {
        //                    ""RuleName"": ""CheckIdCardIsEmpty"",
        //                    ""ErrorMessage"": ""身份证号不可以为空."",
        //                     ""ErrorType"": ""Error"",
        //                    ""RuleExpressionType"": ""LambdaExpression"",
        //                    ""Expression"": ""IdCard != null""
        //                  }
        //                ]
        //              }] ";
        //    //输入的数据
        //    var userInput = new UserInput
        //    {
        //        IdCard = "372522197102261491",//虚假身份证号
        //        Age = 18,
        //    };

        //    //反序列化Json格式规则字符串
        //    var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(rulesStr);
        //    var rulesEngine = new RulesEngine.RulesEngine(workflowRules.ToArray(), reSettings);

        //    List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("UserInputWorkflow", userInput);
        //    foreach (var item in resultList)
        //    {
        //        Console.WriteLine("验证成功：{0}，消息：{1}", item.IsSuccess, item.ExceptionMessage);
        //    }

        //    Console.ReadLine();
        //}
        #endregion

        #region demo3多对象组合条件
        public class UserInput
        {
            public int UserId { get; set; }
            public string IdCard { get; set; }
            public int Age { get; set; }
            public class ListItem
            {
                public int Id { get; set; }
                public string Value { get; set; }
            }
        }

        [Obsolete]
        static async Task Main(string[] args)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "Rules.json", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
                throw new Exception("Rules not found.");

            var rulesStr = File.ReadAllText(files[0]);

            var userInput = new UserInput
            {
                UserId = 1,
                IdCard = "372522197102261491",//虚假身份证号
                Age = 18
            };
            var input = new
            {
                user = userInput,
                items = new List<ListItem>()
                {
                    new ListItem{ Id=1,Value="first"},
                    new ListItem{ Id=2,Value="second"}
                }
            };

            //反序列化Json格式规则字符串
            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(rulesStr);
            var rulesEngine = new RulesEngine.RulesEngine(workflowRules!.ToArray());

            List<RuleResultTree> resultList = await rulesEngine.ExecuteAllRulesAsync("UserInputWorkflow", input);
            foreach (var item in resultList)
            {
                Console.WriteLine("验证成功：{0}，消息：{1}", item.IsSuccess, item.ExceptionMessage);
            }

            Console.ReadLine();

        }
        #endregion
    }


}