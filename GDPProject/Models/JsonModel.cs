using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDPProject.Models
{
    public class FieldModel
    {
        public string  title { get; set; }
        public string  Value { get; set; }
        public string min { get; set; }
        public string max { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int page { get; set; }
        public int type { get; set; }

        public string font { get; set; }
    }
    public class JsonModel
    {
        public string operatorid { get; set; }
        public List<FieldModel> model { get; set; }
        public string durationTime { get; set; }
        public DateTime testDate { get; set; }
        public int id { get; set; }
    }

    public class hospitaldata
    {
        public string  title { get; set; }
        public string  address { get; set; }
        public string  ostan { get; set; }
        public string  shahrestan { get; set; }
        public string  phone { get; set; }
        public string  EN { get; set; }
    }
    public class devicedata
    {
        public string  id { get; set; }
        public string  title { get; set; }
        public string  emptyName { get; set; }
        public string  textedName { get; set; }
    }
    public class userdata
    {
        public string ID { get; set; }
        public string code { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
    public class BaseViewModel
    {
        public String username { get; set; }
        public String pass { get; set; }
        public String name { get; set; }
    }
    public class FiltersModel
    {
        public string ID { get; set; }
        public string title { get; set; }
       
    }
    public class RootObjectFilter
    {
        public List<FiltersModel> filtersModel { get; set; }
    }
    public class FeatureData
    {
        public List<SubFeature> subFeatures { get; set; }
        public List<MainFeature> mainFeatures { get; set; }

    }

    public class FeatureModel
    {
        public List<FeatureData> featureData { get; set; }
    }
    public class SubFeature
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public string MasterID { get; set; }
        public string  page { get; set; }
    }

    public class MainFeature
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string value { get; set; }
        public string MasterID { get; set; }
    }

}