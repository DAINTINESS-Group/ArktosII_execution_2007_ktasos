Public MustInherit Class Loader
    Protected Arktos As Constructs.Core

    Sub New(ByRef CurrentArktos As Constructs.Core)
        Arktos = CurrentArktos
        '"Load" function assumes that nothing is already loaded,
        'so whatever is in memory should be dumped
        Arktos.Clear()
    End Sub

    Public MustOverride Sub Load()
End Class
