# LOLKiller

可以输入进程名，以及查杀周期

周期性的扫描进程列表，如果发现同名进程，那么就杀掉该进程

小bug：

1.TextBox的PreviewKeyDown事件用于限制除数字外的其他字符输入，如果发现其他字符，那么就将该事件Handler的值设为true，此时需要让TextBox重新获取焦点才能继续输入

2.查杀周期建议2s以上，当查杀过于频繁（默认会查杀任务管理器进程，即Taskmgr）时会报Exception（可能是触发了系统的保护机制）

3.运行后即关闭主窗体，仅能在任务管理器里找到并关闭，然而任务管理器也被我干掉了233，谨慎设置查杀周期！
