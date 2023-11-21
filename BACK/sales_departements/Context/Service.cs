// using System;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using sales_departements.Models;

// namespace sales_departements.Context;

// public class Service
// {

//     public static string Serialize(List<object> objects) {
//         var options = new JsonSerializerOptions
//         {
//             ReferenceHandler = ReferenceHandler.Preserve,
//         };

//         var jsonString = JsonSerializer.Serialize(objects, options);
//         //var jsonString = JsonConvert.SerializeObject(objects);
//         return jsonString;
//     }

// }
