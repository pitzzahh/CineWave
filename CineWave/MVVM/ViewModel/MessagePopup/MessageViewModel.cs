using System.Collections.Generic;
using System.Collections.ObjectModel;
using CineWave.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using org.russkyc.moderncontrols;

namespace CineWave.MVVM.ViewModel.MessagePopup;

public partial class MessageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? _message;
    [ObservableProperty]
    private bool _isYes;
    [ObservableProperty]
    private bool _isNo;

    private readonly Message _messageView;
    
    public Type MessageType { get; set; } = Type.Information;

    private readonly ObservableCollection<ModernButton> _modernButtons = new(); // For seats choose
    public IEnumerable<ModernButton> Options => _modernButtons;


    public MessageViewModel(Message messageView)
    {
        _messageView = messageView;
    }

    private void AddButton()
    {
        switch (MessageType)
        {
            case Type.Information:
                var okButton = new ModernButton
                {
                    Content = "OK"
                };
                okButton.Click += (_, _) => ButtonClicked("OK");
                _modernButtons.Add(okButton);
                break;
            case Type.Question:
                var yesButton = new ModernButton
                {
                    Content = "Yes"
                };
                yesButton.Click += (_, _) => ButtonClicked("Yes");
                var noButton = new ModernButton
                {
                    Content = "No"
                };
                noButton.Click += (_, _) => ButtonClicked("No");
                _modernButtons.Add(yesButton);
                _modernButtons.Add(noButton);
                break;
            case Type.Warning:
            case Type.Error:
                var okButtonWarning = new ModernButton
                {
                    Content = "OK"
                };
                okButtonWarning.Click += (_, _) => ButtonClicked("OK");
                _modernButtons.Add(okButtonWarning);
                break;
        }
    }

    private void ButtonClicked(string buttonText)
    {
        switch (buttonText)
        {
            case "Yes":
                IsYes = true;
                break;
            case "No":
                IsNo = true;
                break;
        }
    }
    
    public void ShowMessageWindow()
    {
        AddButton();
        _messageView.Topmost = true;
        _messageView.Show();
        _messageView.Focus();
    }
    
    public enum Type
    {
        Information,
        Question,
        Warning,
        Error
    }
}