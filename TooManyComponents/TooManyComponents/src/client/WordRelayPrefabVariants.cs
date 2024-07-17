using JimmysUnityUtilities;
using LogicWorld.References;
using LogicWorld.Rendering.Dynamics;
using LogicWorld.SharedCode.Components;
using System.Collections.Generic;
using LogicAPI.Data;
using UnityEngine;

namespace TooManyComponents.Client.ClientCode
{
    public class WordRelayPrefabGenerator : DynamicPrefabGenerator<int>
    {
        private byte wordSize;

        public override void Setup(ComponentInfo info)
        {
            wordSize = (byte) info.CodeInfoInts[0];
        }

        public override (int inputCount, int outputCount) GetDefaultPegCounts()
            => (wordSize * 8 * 2 + 1, 0);

        protected override int GetIdentifierFor(ComponentData componentData)
            => 0;

        protected override Prefab GeneratePrefabFor(int _)
        {
            List<Block> blocks = new List<Block>();
            blocks.Add(
                new Block
                {
                    RawColor = new Color24(0x7E133B),
                    Position = new Vector3(-0.5f, 0, -0.5f),
                    Scale = new Vector3(2, 1, wordSize * 8),
                    Mesh = Meshes.OriginCube
                }
            );
            List<ComponentInput> inputs = new List<ComponentInput>();
            for (int i = 0; i < wordSize * 8; i++)
            {
                inputs.Add(
                    new ComponentInput
                    {
                        Position = new Vector3(-0.5f, 0.5f, i),
                        Rotation = new Vector3(0f, 0f, 90f),
                        Length = 0.6f
                    }
                    );
            }
            for (int i = 0; i < wordSize * 8; i++)
            {
                inputs.Add(
                    new ComponentInput
                    {
                        Position = new Vector3(1.5f, 0.5f, i),
                        Rotation = new Vector3(0f, 0f, -90f),
                        Length = 0.6f
                    }
                    );
            }
            inputs.Add(
                new ComponentInput
                {
                    Position = new Vector3(0.5f, 1f, wordSize * 4 - 0.5f)
                }
                );

            return new Prefab
            {
                Blocks = blocks.ToArray(),
                Inputs = inputs.ToArray(),
            };
        }
    }
}