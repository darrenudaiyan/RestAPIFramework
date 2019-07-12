using Rest.API.Framework.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rest.API.Framework.DataSource
{
    public class RestAPIDataSource
    {
        private static RestAPIDataSource instance = null;
        private string appPath;

        public List<Whiskey> Whiskeys { get; set; }
        public List<Assay> Assays { get; set; }

        public static RestAPIDataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RestAPIDataSource();
                }
                return instance;
            }
        }
        
        private RestAPIDataSource()
        {
            this.Reset();
            this.Initialize();
        }

        public void Reset()
        {
            this.Whiskeys = new List<Whiskey>();
            this.Assays = new List<Assay>();
        }

        public void Initialize()
        {
            appPath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "App_Data");
            InitializeAssays();
            InitializeWhiskeys();
        }

        public void InitializeAssays()
        {
            var assaysList = File.ReadLines(appPath + @"\Assays.txt");
            foreach (var line in assaysList)
            {
                var assaysData = line.Split(',');
                if (assaysData.Count() == 0) continue;
                var assayData = new Assay()
                {
                    AssayId = assaysData[0],
                    Name = assaysData[1],
                    Percent = assaysData[2],
                    SpecificGravity = assaysData[3]
                };
                Assays.Add(assayData);
            }
        }

        public void InitializeWhiskeys()
        {
            var WhiskeyList = File.ReadLines(appPath + @"\Whiskeys.txt");
            foreach (var line in WhiskeyList)
            {
                var WhiskeysData = line.Split(',');
                if (WhiskeysData.Count() == 0) continue;
                var WhiskeyData = new Whiskey()
                {
                    WhiskeyId = WhiskeysData[0],
                    Name = WhiskeysData[1],
                    Assays = GetAssayData(WhiskeysData)
                };
                Whiskeys.Add(WhiskeyData);
            }
        }

        private List<Assay> GetAssayData(IEnumerable<string> WhiskeysData)
        {
            var WhiskeyAssays = WhiskeysData.Skip(2).ToList();
            var assayData = new List<Assay>();
            foreach (var assay in Assays)
            {
                if (WhiskeyAssays.Contains(assay.AssayId))
                {
                    assayData.Add(assay);
                }
            }
            return assayData;
        }
    }
}