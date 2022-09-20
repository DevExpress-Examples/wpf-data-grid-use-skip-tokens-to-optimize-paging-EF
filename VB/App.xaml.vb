Imports DevExpress.Internal
Imports System.Windows

Namespace InfiniteAsyncSourceSkipTokenEFSample

    ''' <summary>
    ''' Interaction logic for App.xaml
    ''' </summary>
    Public Partial Class App
        Inherits Application

        Public Sub New()
            Call DbEngineDetector.PatchConnectionStringsAndConfigureEntityFrameworkDefaultConnectionFactory()
        End Sub
    End Class
End Namespace
