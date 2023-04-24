using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using System;
using System.Windows.Input;

namespace Flip_Chess
{
    partial class MainPage
    {

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public bool CanExecute(object parameter) => parameter != default;
        public void Execute(object parameter)
        {
            if (parameter is OptionType item)
            {
                this.Click(item);
            }
        }

        public ICommand Command => this;
        public void Click(OptionType type)
        {
            switch (type)
            {
                case OptionType.Ai:
                    if (this.IsBlack) this.ItemClick(new BlackAutoAICollection(this).FindAutoAI(this));
                    else if (this.IsRed) this.ItemClick(new RedAutoAICollection(this).FindAutoAI(this));
                    break;
                case OptionType.Play:
                    this.State = GameState.None;
                    break;

                case OptionType.Lose:
                    this.SelectedIndex = 1;
                    this.State = GameState.Lose;
                    break;
                case OptionType.Win:
                    this.SelectedIndex = 1;
                    this.State = GameState.Win;
                    break;
                case OptionType.Pause:
                    this.SelectedIndex = 1;
                    this.State = GameState.Pause;
                    break;

                case OptionType.Continue:
                    this.State = GameState.None;
                    break;
                case OptionType.Restart:
                    this.Historian.Clear();
                    this.HistorianCount = 0;
                    this.SaveStep(this.Step);

                    //this.Randoms.Random();
                    this.Collection.Clear();
                    //this.Chesses.Clear();

                    this.BeginClip(); /// <see cref="OptionType.UIClipCompleted"/>
                    this.Relive();
                    this.State = GameState.None;

                    this.HideFlip();
                    this.StopFlip();

                    this.HideCapture();
                    this.StopCapture();

                    this.HideCemetery();
                    this.StopCemetery();
                    break;
                case OptionType.Next:
                    this.Randoms.Home();
                    this.Randoms.Random();
                    this.WriteRandom(); // Sertings

                    this.Click(OptionType.Restart);
                    break;

                case OptionType.FullScreen:
                    this.ClickFullScreen();
                    break;
                case OptionType.UnFullScreen:
                    this.ClickUnFullScreen();
                    break;

                case OptionType.About:
                    this.SelectedIndex = 0;
                    break;
                case OptionType.Tutorial:
                    this.SelectedIndex = 2;
                    break;

                case OptionType.Step1:
                    this.SelectedIndex = 3;
                    break;
                case OptionType.Step2:
                    this.SelectedIndex = 4;
                    break;
                case OptionType.Step3:
                    this.SelectedIndex = 5;
                    break;
                case OptionType.Step4:
                    this.State = GameState.None;
                    break;

                case OptionType.UIFlipCompleted: /// <see cref="OptionType.UIFlipCompleted"/>
                    this.Shown();
                    break;
                case OptionType.UICaptureCompleted: /// <see cref="OptionType.UICaptureCompleted"/>
                    this.Shown();

                    this.BeginCemetery(); /// <see cref="OptionType.UICemeteryCompleted"/>
                    break;
                case OptionType.UICemeteryCompleted: /// <see cref="OptionType.UICemeteryCompleted"/>
                    this.HideCapture();

                    this.Deaded();
                    break;
                case OptionType.UIClipCompleted: /// <see cref="OptionType.UIClipCompleted"/>
                    this.Chesses.Clear();
                    break;

                default:
                    break;
            }
        }

    }
}