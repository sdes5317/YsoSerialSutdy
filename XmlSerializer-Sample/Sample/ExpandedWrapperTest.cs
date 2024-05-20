using System;
using System.Collections.Generic;
using System.Data.Services.Internal;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializer_Sample.Sample
{
    public class ExpandedWrapperTest
    {
        public static void Run()
        {
            Foo foo = CreateFooObject();

            // Will fail to deserialize because the Bar property is of type object
            SerializeAndDeserializeWithoutWrapper(foo);
            // Will succeed to deserialize because the Bar property is of type Bar
            SerializeAndDeserializeWithWrapper(foo);
        }


        public static Foo CreateFooObject()
        {
            // 創建 Foo 對象，並初始化其 Bar 屬性
            return new Foo
            {
                Bar = new Bar { Name = "NestedBar" },
                Description = "This is a Foo object"
            };
        }

        public static void SerializeAndDeserializeWithoutWrapper(Foo foo)
        {
            // 使用 XmlSerializer 序列化 Foo 對象
            var serializer = new XmlSerializer(typeof(Foo));
            string xml;

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, foo);
                xml = writer.ToString();
                Console.WriteLine("Serialized XML without wrapper:");
                Console.WriteLine(xml);
            }

            // 嘗試使用 XmlSerializer 反序列化 Foo 對象
            Foo deserializedFoo;

            try
            {
                using (var reader = new StringReader(xml))
                {
                    deserializedFoo = (Foo)serializer.Deserialize(reader);
                }

                // 驗證反序列化結果
                Console.WriteLine("\nDeserialized Foo object:");
                Console.WriteLine($"Description: {deserializedFoo.Description}");
                Console.WriteLine($"Bar Name: {(deserializedFoo.Bar as Bar)?.Name}");
            }
            catch (Exception ex)
            {
                // 捕捉反序列化失敗的異常
                Console.WriteLine("\nDeserialization failed:");
                Console.WriteLine(ex);
            }
        }

        public static void SerializeAndDeserializeWithWrapper(Foo foo)
        {
            // 創建 ExpandedWrapper 對象
            var wrapper = new ExpandedWrapper<Foo, Bar>
            {
                ExpandedElement = foo,
                ProjectedProperty0 = (Bar)foo.Bar
            };

            // 使用 XmlSerializer 序列化 ExpandedWrapper 對象
            var serializer = new XmlSerializer(typeof(ExpandedWrapper<Foo, Bar>));
            string xml;

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, wrapper);
                xml = writer.ToString();
                Console.WriteLine("Serialized XML with wrapper:");
                Console.WriteLine(xml);
            }

            // 使用 XmlSerializer 反序列化 ExpandedWrapper 對象
            ExpandedWrapper<Foo, Bar> deserializedWrapper;

            try
            {
                using (var reader = new StringReader(xml))
                {
                    deserializedWrapper = (ExpandedWrapper<Foo, Bar>)serializer.Deserialize(reader);
                }

                // 驗證反序列化結果
                Console.WriteLine("\nDeserialized ExpandedWrapper object:");
                Console.WriteLine($"Description: {deserializedWrapper.ExpandedElement.Description}");
                Console.WriteLine($"Bar Name: {deserializedWrapper.ProjectedProperty0.Name}");
            }
            catch (Exception ex)
            {
                // 捕捉反序列化失敗的異常
                Console.WriteLine("\nDeserialization failed:");
                Console.WriteLine(ex);
            }
        }
    }

    public class Bar
    {
        public string Name { get; set; }
    }

    public class Foo
    {
        public object Bar { get; set; }
        public string Description { get; set; }
    }
}
