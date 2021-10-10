using Core.ControllerRegistrator;
using Core.PluginBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Net.Http.Headers;

namespace Project1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PluginController : ControllerBase
    {
        public IPluginManager PluginManager;
        public ApplicationPartManager ApplicationPartManager {  get; set; }
        private IControllerRegistrator ControllerRegistrator {  get; set; }
        public PluginController(IPluginManager pluginManager, ApplicationPartManager configuration, IControllerRegistrator controllerRegistrator)
        {
            PluginManager = pluginManager;
            ApplicationPartManager = configuration;
            ControllerRegistrator = controllerRegistrator;
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Plugins");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    PluginManager.LoadControllerPlugin(fullPath);

                    var assembly = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath(fullPath);

                    ApplicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
                    MyActionDescriptorChangeProvider.Instance.HasChanged = true;
                    MyActionDescriptorChangeProvider.Instance.TokenSource.Cancel();

                    //ControllerRegistrator.registerNewController(PluginManager.ControllerPluginsCommands.Where(x => x.PluginContract.pluginId == "TestPlugin").FirstOrDefault().getController());
                    


                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
