using Nakama.TinyJson;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private int money;
    [SerializeField] private float income;
    [SerializeField] private float populationGrowth;
    public int[] TilesOwned;

    // from https://github.com/Argus-Labs/agar-shooter/blob/main/Client/Assets/Scripts/Player.cs
    public void ReceiveNewMsg(ServerPayload payload)
    {
        // delete all the inputs that have been processed by the server
        var j = 0;
        while (j < pendingInputs.Count)
        {
            var input = pendingInputs[j];
            if (input.input.Input_sequence_number < payload.lastProcessedInput)
            {
                // Already processed. Its effect is already taken into account into the world update
                // we just got, so we can drop it.
                // pendingInputs.splice(j, 1);
                pendingInputs.Dequeue();
            }
            else if (input.input.Input_sequence_number == payload.lastProcessedInput)
            {
                if (Vector2.Distance(payload.pos, input.position) < 0.05f)
                {
                    // Already processed. Its effect is already taken into account into the world update
                    // we just got, so we can drop it.
                    pendingInputs.Dequeue();
                    break;
                }

                pendingInputs.Dequeue();
                // print expected pos and actual pos and sequence number
                Debug.Log("there is a difference:" + "Server pos:" + payload.pos + "player pos:" + input.position +
                          "sequence number:" + payload.lastProcessedInput + "current sequence number:" +
                          sequenceNumber);
                pos = payload.pos;
            }
            else
            {
                // Not processed by the server yet. Re-apply it.
                ApplyInput(input.input, input.deltaTime);
                // update the player position in the pendingInputs
                pendingInputs[j] = new PlayerInputExtraInfo(input.input, input.deltaTime, pos);
                j++;
            }
        }
    }
}