# Threading.ResetEventExtensions

## What's?

AutoResetEvent.WaitOne を非同期に実行するためのライブラリ。

スレッドセーフにしたい&ブロックしたくないが最大の動機なので、パフォーマンスは全く考慮していない。

## How to use?

1. Nugetで "rhsgw/Threading.ResetEventExtensions" をインストールする
2. using Threading.ResetEventExtensions;
3. スレッド間で共有するAutoResetEventを用意しておく
4. 次のような感じで使用する
```
using(var waited = await autoResetEvent.WaitOneAsync(5000)) // タイムアウト5000ms
{
  if(!waited.Signal) return; // タイムアウト
  // シグナル状態になったのでなんか処理
}
```
