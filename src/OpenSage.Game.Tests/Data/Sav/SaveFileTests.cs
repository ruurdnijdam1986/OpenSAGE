﻿using System;
using System.Collections.Generic;
using System.IO;
using OpenSage.Data.Sav;
using Xunit;

namespace OpenSage.Tests.Data.Sav
{
    public class SaveFileTests : IClassFixture<GameFixture>
    {
        private static readonly string RootFolder = Path.Combine(Environment.CurrentDirectory, "Data", "Sav", "Assets");

        private readonly GameFixture _fixture;

        public SaveFileTests(GameFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory(Skip = "Doesn't work yet")]
        //[Theory]
        [MemberData(nameof(GetSaveFiles))]
        public void CanLoadSaveFiles(string relativePath)
        {
            var fullPath = Path.Combine(RootFolder, relativePath);

            using var stream = File.OpenRead(fullPath);

            var sageGame = Enum.Parse<SageGame>(relativePath.Substring(0, relativePath.IndexOf(Path.DirectorySeparatorChar)));

            var game = _fixture.GetGame(sageGame);

            SaveFile.LoadFromStream(stream, game);

            game.EndGame();
        }

        public static IEnumerable<object[]> GetSaveFiles()
        {
            foreach (var file in Directory.GetFiles(RootFolder, "*.sav", SearchOption.AllDirectories))
            {
                var relativePath = file.Substring(RootFolder.Length + 1);
                yield return new object[] { relativePath };
            }
        }
    }
}
