using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Scriptingo.Admin.Managers;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;

namespace Scriptingo.Admin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ExecuteController : BaseLoginedController
    {
        [HttpPost("InsertProcess")]
        public ApiReturn InsertProcess([FromForm] _process p)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbProcess = new FastApiContext<_process>();
                dbProcess.Add(p);
                dbProcess.SaveChanges();
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }
        [HttpPost("UpdateProcess")]
        public ApiReturn UpdateProcess([FromForm] _process p)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbProcess = new FastApiContext<_process>();
                var pEntity = dbProcess.Data.FirstOrDefault(x => x.ID == p.ID);
                if (pEntity != null)
                {
                    pEntity.sql = p.sql;
                    pEntity.name = p.name;
                    pEntity.description = p.description;
                    pEntity.con_id = p.con_id;
                    dbProcess.Entry(pEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbProcess.SaveChanges();
                }
                else
                {
                    apiReturn.status = "error";
                    apiReturn.message = "Process could not found. id:" + p.ID;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }
        [HttpPost("DeleteProcess")]
        public ApiReturn DeleteProcess([FromForm] _process p)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbProcess = new FastApiContext<_process>();
                var pEntity = dbProcess.Data.FirstOrDefault(x => x.ID == p.ID);
                if (pEntity != null)
                {
                    dbProcess.Data.Remove(pEntity);
                    dbProcess.SaveChanges();
                }
                else
                {
                    apiReturn.status = "error";
                    apiReturn.message = "Process could not found. id:" + p.ID;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }

        [HttpPost("ListProcess")]
        public ApiReturn ListProcess([FromBody] GridAjaxParam data = null)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var apiCode = data.Parameters.FirstOrDefault(x => x.name == "api_code").value;
                var apiCodeVal = Convert.ToInt64(apiCode);
                var dbApi = new FastApiContext<_api>();
                var api = dbApi.Data.FirstOrDefault(x => x.api_code == apiCodeVal && x.user_id == CurrentUserID().Value);
                if (api == null)
                {
                    apiReturn = new ApiReturn()
                    {
                        status = "error",
                        message = "Api not exist or you are not owner.",
                    };
                }
                else
                {
                    var dbCon = new FastApiContext<_con>();
                    var dbProcess = new FastApiContext<_process>();
                    var conList = dbCon.Data.Where(x => x.api_id == api.ID).ToList();
                    var dbConType = new FastApiContext<_con_type>();
                    var conTypeList = dbConType.Data.ToList();
                    var pList = dbProcess.Data.ToList();
                    var systemConnectionType = Config.Get().MainDbType;
                    foreach (var p in pList)
                    {

                        var con = conList.FirstOrDefault(x => x.ID == p.con_id);
                        var contype = conTypeList.FirstOrDefault(x => x.ID == con.con_type_id);
                        p.con_type_name = contype.name;
                        p.con_name = con.name;

                    }
                    apiReturn.data = pList;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }

        [HttpPost("ListApi")]
        public ApiReturn ListApi([FromForm] GridAjaxParam data = null)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            var user = CurrentUser();
            var dbApi = new FastApiContext<_api>();
            var apis = dbApi.Data.Where(x => x.user_id == user.ID);
            apiReturn.data = apis.ToList();

            return apiReturn;
        }



        [HttpPost("InsertCon")]
        public ApiReturn InsertCon([FromForm] _con c)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbCon = new FastApiContext<_con>();
                dbCon.Add(c);
                dbCon.SaveChanges();
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }
        [HttpPost("UpdateCon")]
        public ApiReturn UpdateCon([FromForm] _con c)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbCon = new FastApiContext<_con>();
                var pEntity = dbCon.Data.FirstOrDefault(x => x.ID == c.ID);
                if (pEntity != null)
                {
                    pEntity.connection = c.connection;
                    pEntity.name = c.name;
                    pEntity.connection = c.connection;
                    pEntity.active = c.active;
                    pEntity.con_type_id = c.con_type_id;
                    dbCon.Entry(pEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbCon.SaveChanges();
                }
                else
                {
                    apiReturn.status = "error";
                    apiReturn.message = "Process could not found. id:" + c.ID;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }
        [HttpPost("DeleteCon")]
        public ApiReturn DeleteCon([FromForm] _process p)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var dbProcess = new FastApiContext<_process>();
                var pEntity = dbProcess.Data.FirstOrDefault(x => x.ID == p.ID);
                if (pEntity != null)
                {
                    dbProcess.Data.Remove(pEntity);
                    dbProcess.SaveChanges();
                }
                else
                {
                    apiReturn.status = "error";
                    apiReturn.message = "Process could not found. id:" + p.ID;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }

        [HttpPost("ListCon")]
        public ApiReturn ListCon([FromBody] GridAjaxParam data = null)
        {
            var apiReturn = new ApiReturn()
            {
                status = "success"
            };
            try
            {
                var apiCode = data.Parameters.FirstOrDefault(x => x.name == "api_code").value;
                var apiCodeVal = Convert.ToInt64(apiCode);
                var dbApi = new FastApiContext<_api>();
                var api = dbApi.Data.FirstOrDefault(x => x.api_code == apiCodeVal && x.user_id == CurrentUserID().Value);
                if (api == null)
                {
                    apiReturn = new ApiReturn()
                    {
                        status = "error",
                        message = "Api not exist or you are not owner.",
                    };
                }
                else
                {
                    var dbCon = new FastApiContext<_con>();
                    var dbConType = new FastApiContext<_con_type>();
                    var types = dbConType.Data.ToList();
                    var conList = dbCon.Data.Where(x => x.api_id == api.ID).ToList();
                    foreach (var con in conList)
                    {
                        con.con_type_name = types.FirstOrDefault(x => x.ID == con.con_type_id).name;
                    }
                    apiReturn.data = conList;
                }
            }
            catch (Exception ex)
            {
                apiReturn.status = "error";
                apiReturn.message = ex.Message;
            }

            return apiReturn;
        }
        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                System.IO.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        [HttpGet("Build")]
        public ApiReturn Build(int api_code)
        {
            var apiReturn = new ApiReturn();
            try
            {
                var dbApi = new FastApiContext<_api>();
                var dbApiBuild = new FastApiContext<_api_build>();

                var api = dbApi.Data.FirstOrDefault(x => x.user_id == CurrentUserID() && x.api_code == api_code);
                if (api == null)
                {
                    apiReturn.status = "error";
                    apiReturn.message = T("Api is not exist or you dont have ownership.");
                    return apiReturn;
                }


                long currentBuildNumber = 0;
                var lastMaxBuild = dbApiBuild.Data.Where(x => x.api_id == api.ID)
                    .OrderByDescending(x => x.build_number)
                    .FirstOrDefault();
                if (lastMaxBuild != null)
                {
                    currentBuildNumber = lastMaxBuild.build_number + 1;
                }
                else
                {
                    currentBuildNumber = 1;
                }
                var config = Config.Get();
                var buildGuid = Guid.NewGuid();
                var buildName = api.name + "_build" + currentBuildNumber + "_" + buildGuid.ToString();
                string buildDir = config.BuildDir + buildName + "\\Code\\";

                var dbCon = new FastApiContext<_con>();
                var dbProcess = new FastApiContext<_process>();
                var dbConType = new FastApiContext<_con_type>();
                var dbConTable = new FastApiContext<_con_table>();


                var conList = dbCon.Data.Where(x => x.active == true && x.api_id == api.ID && x.is_scaffold == true).ToList();
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var buildApiDir = buildDir + "Api\\";
                var buildCommonDir = buildDir + "Common\\";
                string modelsOutputDir = buildDir + "Common\\Models\\";
                string controllersOutputDir = buildDir + "Api\\Controllers\\";

                // klasördeki eski dosyaları temizliyoruz.
                var cleanDirs = new List<string>() { modelsOutputDir, controllersOutputDir };

                // template dosyalarından okuyup hazır hale getiriyoruz.
                var processControllerTemplate = System.IO.File.ReadAllText(config.IDEProjectDir + "ControllerTemplate_Process.txt");
                var processControllerActionTemplate = System.IO.File.ReadAllText(config.IDEProjectDir + "ControllerTemplate_ProcessItem.txt");

                // build klasörüne varolan projenin bir kopyasını oluşturuyoruz.
                CopyFilesRecursively(config.CodeDir, buildDir);


                // auth connection
                var mainCon = dbCon.Data.Where(x => x.api_id == api.ID && x.is_main == true).FirstOrDefault();
                var mainDbType = dbConType.Data.FirstOrDefault(x => x.ID == mainCon.con_type_id);

                // temizlenecek klasörler temizleniyor.
                foreach (var clean in cleanDirs)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(clean);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                }
                var apiConfig = Config.Get(buildApiDir);
                apiConfig.RegisterToken = (Guid.NewGuid().ToString()).Replace("-", "");
                // ana db türü ve connection string i ekliyoruz.
                apiConfig.MainDbType = mainDbType.name;
                apiConfig.MainDbConnectionString = mainCon.ConStr();

                if (apiConfig.FastApiConnections != null)
                    apiConfig.FastApiConnections.Clear();// tümünü temizliyoruz.
                else
                    apiConfig.FastApiConnections = new List<DbConnection>();

                var processList = dbProcess.Data.ToList();


                foreach (var con in conList)
                {

                    var conType = dbConType.Data.FirstOrDefault(x => x.ID == con.con_type_id);
                    apiConfig.FastApiConnections.Add(new DbConnection()
                    {
                        name = con.name,
                        dbType = conType.name,
                        schema = con.db_schema,
                        connectionString = con.ConStr()
                    });
                    string dbType = conType.name;
                    string connectionString = con.ConStr();
                    string commandTemplate = "/C dotnet ef dbcontext scaffold \"{con}\" {package} -o {path} --no-pluralize --use-database-names --schema " + con.db_schema + " --force";
                    string fileName = "cmd.exe";


                    string packageName = "";
                    if (conType.name == Config.MsSql)
                    {
                        packageName = "Microsoft.EntityFrameworkCore.SqlServer";
                    }
                    else if (conType.name == Config.PostgreSql)
                    {
                        packageName = "Npgsql.EntityFrameworkCore.PostgreSQL";
                    }
                    else if (conType.name == Config.Oracle)
                    {
                        packageName = "Oracle.EntityFrameworkCore";
                    }
                    else if (conType.name == Config.MySql)
                    {
                        packageName = "MySql.EntityFrameworkCore";
                    }
                    else if (conType.name == Config.Sqlite)
                    {
                        packageName = "Microsoft.EntityFrameworkCore.Sqlite";
                    }

                    if (string.IsNullOrEmpty(con.db_schema))
                        commandTemplate = commandTemplate.Replace("--schema", "");


                    string cmd = commandTemplate.Replace("{con}", connectionString);
                    cmd = cmd.Replace("{package}", packageName);
                    cmd = cmd.Replace("{path}", modelsOutputDir + con.name);

                    var result = CMDHelper.RunExternalExe(fileName, buildDir + "Common\\", cmd);

                    var files = Directory.GetFiles(modelsOutputDir + con.name);

                    var controllerTemplatePath = config.IDEProjectDir + "ControllerTemplate.txt";
                    var controllerTemplate = System.IO.File.ReadAllText(controllerTemplatePath);
                    var list = new List<CodeData>();
                    foreach (var file in files)
                    {
                        if (file.EndsWith("Context.cs"))
                        {
                            System.IO.File.Delete(file);
                            continue;
                        }
                        var className = file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\") - 1).Replace(".cs", "");

                        if (dbConTable.Data.Count(x => x.name == className) == 0)
                        {
                            dbConTable.Data.Add(new _con_table()
                            {
                                active = true,
                                con_id = con.ID,
                                name = className
                            });
                            dbConTable.SaveChanges();
                        }

                        string schemaAttr = "";
                        if (!string.IsNullOrEmpty(con.db_schema))
                            schemaAttr = @", Schema = """ + con.db_schema + @"""";


                        var classNameLast = con.name + "_" + className;
                        string template = @"[GeneratedController(""api/" + className + @"/[action]"")]
[Table(""" + className + @"""" + schemaAttr + @")]
[FastApiTable(""" + con.name + @""",""" + conType.name + @""")]";

                        var fileContent = System.IO.File.ReadAllText(file);
                        fileContent = fileContent.Replace("public partial class " + className, template + "\npublic partial class " + className + " : BaseModel");
                        fileContent = fileContent.Replace("namespace Scriptingo.Common.Models." + con.name + ";", "using System.ComponentModel.DataAnnotations.Schema;\nusing Scriptingo.Common;\n\nnamespace Scriptingo.Models." + con.name + ";");
                        fileContent = fileContent.Replace("public int ID { get; set; }", "");
                        fileContent = fileContent.Replace("public string ID { get; set; }", "");
                        fileContent = fileContent.Replace("public long ID { get; set; }", "");
                        fileContent = fileContent.Replace("public decimal ID { get; set; }", "");
                        System.IO.File.WriteAllText(file, fileContent);
                        string controllText = controllerTemplate.Replace("{db_name}", con.db_name)
                            .Replace("{class_name}", className)
                            .Replace("{namespace}", con.name)
                            .Replace("{comment}", con.comment);
                        if (!Directory.Exists(controllersOutputDir + con.name))
                            Directory.CreateDirectory(controllersOutputDir + con.name);
                        System.IO.File.WriteAllText(controllersOutputDir + con.name + "\\" + con.name + "_" + className + "Controller.cs", controllText);
                    }



                    //process
                    string processControllerText = processControllerTemplate;
                    string processActionText = "";
                    foreach (var item in processList.Where(x => x.con_id == con.ID))
                    {
                        processActionText += (processControllerActionTemplate
                            .Replace("{process_name}", item.name)
                            .Replace("{sql}", item.sql)
                            .Replace("{process_description}", item.description)
                            + "\n\n");
                    }
                    processControllerText = processControllerText
                        .Replace("{config_name}", con.name)
                        .Replace("{con_comment}", con.comment)
                        .Replace("{process}", processActionText)
                        ;
                    System.IO.File.WriteAllText(controllersOutputDir + con.name + "Controller.cs", processControllerText);
                }
                apiConfig.Jwt = new Common.Jwt()
                {
                    Audience = api.host,
                    Issuer = api.host,
                    Key = (Guid.NewGuid().ToString()).Replace("-", "")
                };
                apiConfig.Save(buildApiDir);

                ZipFile.CreateFromDirectory(buildDir, config.IDEProjectDir + "wwwroot\\Builds\\" + buildName + ".zip");

                apiReturn.status = "success";
                apiReturn.data = "/builds/" + buildName + ".zip";
            }
            catch (Exception ex)
            {

                apiReturn.status = "error";
                apiReturn.message=ex.Message;
            }

            return apiReturn;
        }
        private _user CurrentUser()
        {
            return SessionManager.GetUser(HttpContext);
        }
        private long? CurrentUserID()
        {
            return SessionManager.GetUserId(HttpContext);
        }
    }
}
