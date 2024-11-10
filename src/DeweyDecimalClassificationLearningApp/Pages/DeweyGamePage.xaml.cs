using DeweyDecimalClassification.Vms;

namespace DeweyDecimalClassificationLearningApp.Pages;

public partial class DeweyGamePage : ContentPage
{
    public DeweyGamePage(DeweyGameVm deweyGameVm)
    {
        InitializeComponent();
        BindingContext = deweyGameVm;
    }
}