using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SuperNewRoles.Replay.ReplayActions;
public class ReplayActionMurder : ReplayAction
{
    public byte sourcePlayer;
    public byte targetPlayer;
    public override void ReadReplayFile(BinaryReader reader)
    {
        ActionTime = reader.ReadSingle();
        //ここにパース処理書く
        sourcePlayer = reader.ReadByte();
        targetPlayer = reader.ReadByte();
    }
    //アクション実行時の処理
    public override void OnAction()
    {
        PlayerControl source = ModHelpers.PlayerById(sourcePlayer);
        PlayerControl target = ModHelpers.PlayerById(targetPlayer);
        if (source == null || target == null) {
            Logger.Info($"アクションを実行しようとしましたが、対象がいませんでした。source:{sourcePlayer},target:{targetPlayer}");
            return;
        }
        source.MurderPlayer(target);
    }
    //試合内でアクションがあったら実行するやつ
    public static ReplayActionMurder Create(byte sourcePlayer, byte targetPlayer)
    {
        if (Recorder.IsReplayMode) return null;
        ReplayActionMurder action = new();
        Recorder.ReplayActions.Add(action);
        //ここで秒数指定
        action.ActionTime = Recorder.ReplayActionTime;
        Recorder.ReplayActionTime = 0f;
        //初期化
        action.sourcePlayer = sourcePlayer;
        action.targetPlayer = targetPlayer;
        return action;
    }
}