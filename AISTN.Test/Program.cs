// See https://aka.ms/new-console-template for more information
using AISTN.Test.Services;

Console.WriteLine("Hello, World!");

var s = new ApiTestService();

await s.TestCourtLegacyApi();

//await s.TestGuidsInFile();