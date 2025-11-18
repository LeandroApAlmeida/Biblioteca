using Library.Data;
using Library.Db.Models;
using Library.Services.Session;
using Library.Utils;
using Microsoft.EntityFrameworkCore;


namespace Library.Services.User {


    /// <summary>
    /// Classe para leitura/gravação das configurações do usuário no banco de dados.
    /// </summary>
    public class SettingsService(ApplicationDbContext context, ISessionService sessionService) : ISettingsService {


        /// <summary> Objeto para acesso ao banco de dados. </summary>
        private readonly ApplicationDbContext _context = context;

        /// <summary> Objeto para gerenciamento da sessão de usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;


        /// <summary>
        /// Obter uma configuração com base na sua chave.
        /// </summary>
        /// <param name="key">Chave da configuração.</param>
        /// <returns>Configuração relacionada.</returns>
        private SettingsModel? GetSetting(string key) {

            UserModel user = _sessionService.GetSessionData()!.User;

            try {
                
                SettingsModel? setting = _context.Settings
                .Include(s => s.User)
                .FirstOrDefault(s =>
                    s.Id == key && s.UserId == user.Id
                );

                if (setting == null) {

                    var trackedUser = _context.Users.Find(user.Id);

                    setting = new SettingsModel {
                        Id = key,
                        User = trackedUser!,
                        StringValue = null,
                        IntValue = null,
                        LongValue = null,
                        FloatValue = null,
                        DoubleValue = null,
                        BoolValue = null
                    };

                    _context.Settings.Add(setting);
                    _context.SaveChanges();

                }

                return setting;

            } catch (Exception ex) {
                return null;
            }

        }


        public string? GetString(string key, string? defaultValue) {
            
            string? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.StringValue != null) {
                    value = setting.StringValue;
                } else {
                    SetString(key, defaultValue);
                    value = defaultValue;
                } 
            }

            return value;

        }


        public int? GetInt(string key, int? defaultValue) {

            int? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.IntValue != null) {
                    value = setting.IntValue;
                } else {
                    SetInt(key, defaultValue);
                    value = defaultValue;
                }
            }

            return value;

        }


        public long? GetLong(string key, long? defaultValue) {

            long? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.LongValue != null) {
                    value = setting.LongValue;
                } else {
                    SetLong(key, defaultValue);
                    value = defaultValue;
                }
            }

            return value;

        }


        public float? GetFloat(string key, float? defaultValue) {

            float? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.FloatValue != null) {
                    value = setting.FloatValue;
                } else {
                    SetFloat(key, defaultValue);
                    value = defaultValue;
                }
            }

            return value;

        }


        public double? GetDouble(string key, double? defaultValue) {

            double? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.DoubleValue != null) {
                    value = setting.DoubleValue;
                } else {
                    SetDouble(key, defaultValue);
                    value = defaultValue;
                }
            }

            return value;

        }


        public bool? GetBoolean(string key, bool? defaultValue) {

            bool? value = defaultValue;

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {
                if (setting.BoolValue != null) {
                    value = setting.BoolValue;
                } else {
                    SetBoolean(key, defaultValue);
                    value = defaultValue;
                }
            }

            return value;

        }


        public void SetString(string key, string? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.StringValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        public void SetInt(string key, int? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.IntValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        public void SetLong(string key, long? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.LongValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        public void SetFloat(string key, float? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.FloatValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        public void SetDouble(string key, double? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.DoubleValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        public void SetBoolean(string key, bool? value) {

            SettingsModel? setting = GetSetting(key);

            if (setting != null) {

                setting.BoolValue = value;

                _context.Entry(setting).State = EntityState.Modified;

                _context.Update(setting);

                _context.SaveChanges();

            }

        }


        void ISettingsService.Reset() {

            SetString(Constants.DISCARDED_TEXT_COLOR_KEY, Constants.DEFAULT_DISCARDED_TEXT_COLOR);
            SetString(Constants.DONATED_TEXT_COLOR_KEY, Constants.DEFAULT_DONATED_TEXT_COLOR);
            SetString(Constants.BORROWED_TEXT_COLOR_KEY, Constants.DEFAULT_BORROWED_TEXT_COLOR);

            SetBoolean(Constants.DISCARDED_BOLD_KEY, false);
            SetBoolean(Constants.DISCARDED_UNDERLINE_KEY, false);
            SetBoolean(Constants.DISCARDED_ITALIC_KEY, false);

            SetBoolean(Constants.DONATED_BOLD_KEY, false);
            SetBoolean(Constants.DONATED_UNDERLINE_KEY, false);
            SetBoolean(Constants.DONATED_ITALIC_KEY, false);

            SetBoolean(Constants.BORROWED_BOLD_KEY, false);
            SetBoolean(Constants.BORROWED_UNDERLINE_KEY, false);
            SetBoolean(Constants.BORROWED_ITALIC_KEY, false);

        }


    }


}