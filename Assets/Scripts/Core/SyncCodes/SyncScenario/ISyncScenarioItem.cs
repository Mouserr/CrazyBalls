namespace Assets.Scripts.Core.SyncCodes.SyncScenario
{



    /// <summary>
    /// Псотулаты:
    /// 0. Элемент ОДНОРАЗОВЫЙ (для повторного запуска нужно создавать новый объект). 
    /// 1. После создания IsComplete принимает значение FALSE
    /// 2. Обробатывать Play или Pause можно только в том случае, если значение IsComplete принимает значение FALSE
    /// 3. IsComplete ОБЯЗАТЕЛЬНО переходит в TRUE в двух случаях: если закончились все опреации метода Play(), если вызвали метод Stop()
    /// 4. Метод Play запускает операцию если она не запущена и в случае если есть поддержка паузы вызов Play должен снимать элемент с режима паузы
    /// </summary>
    public interface ISyncScenarioItem : ISyncOperation
    {
        void Play();

        void Stop();

        void Pause();
    }
}
