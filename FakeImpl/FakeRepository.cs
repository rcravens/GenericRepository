using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Repository.Infrastructure;

namespace Repository.FakeImpl
{
    public class Maps
    {
        public List<MapClass> MapInfo { get; set; }


        public Maps()
        {
            MapInfo = new List<MapClass>();
        }

        public static Maps Deserialize()
        {
            Maps maps = null;
            string filePath = "Repository.FakeImpl.Maps.maps.xml";
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath);
            if(stream!=null)
            {
                using (stream)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof (Maps));
                    maps = (Maps) xmlSerializer.Deserialize(stream);
                }
            }

            return maps;
        }

        public static void Serialize()
        {
            Maps maps = new Maps();
            maps.MapInfo.Add(new MapClass {Type = "Repository.FakeImpl.Person", Id = "Id"});
            maps.MapInfo.Add(new MapClass { Type = "Repository.FakeImpl.Address", Id = "Id" });

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Maps));
            StreamWriter writer = new StreamWriter("c:/temp/test.xml");
            xmlSerializer.Serialize(writer, maps);
            writer.Close();
        }
    }

    public class MapClass
    {
        [XmlAttribute("type")]
        public string Type { get; set;}

        [XmlAttribute("id")]
        public string Id { get; set;}
    }

    public class FakeRepository<TKey, TEntity> : IKeyedRepository<TKey, TEntity> where TEntity : class, IKeyed<TKey>
    {
        private readonly InMemoryDbTable<TKey, TEntity> _table;

        public FakeRepository(InMemoryDbTable<TKey, TEntity> table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            _table = table;
        }

        public bool Add(TEntity entity)
        {
            return _table.Add(entity);
        }

        public bool Add(IEnumerable<TEntity> items)
        {
            bool result = true;
            foreach (TEntity entity in items)
            {
                result = Add(entity) && result;
            }
            return result;
        }

        public bool Update(TEntity entity)
        {
            return _table.Update(entity);
        }

        public bool Delete(TEntity entity)
        {
            return _table.Delete(entity.Id);
        }

        public bool Delete(IEnumerable<TEntity> entities)
        {
            bool result = true;
            foreach (TEntity entity in entities)
            {
                result = Delete(entity) && result;
            }
            return result;
        }

        public TEntity FindBy(TKey id)
        {
            return _table.FindBy(id);
        }

        public IQueryable<TEntity> All()
        {
            return _table.All();
        }

        public int Count()
        {
            return _table.Count();
        }
    }

}
