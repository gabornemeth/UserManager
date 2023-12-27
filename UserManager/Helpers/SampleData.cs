﻿using Newtonsoft.Json;
using UserManager.Contracts.Dtos;

namespace UserManager.Helpers
{
    internal class SampleData
    {
        public static IEnumerable<UserDto> GetUsers()
        {
            var sampleData = File.ReadAllText("sample.json");
            return JsonConvert.DeserializeObject<UserDto[]>(sampleData) ?? [];
        }
    }
}