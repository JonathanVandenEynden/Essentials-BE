using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P3Backend.Model.Users
{
    public class DeviceTokens
    {
        public int Id { get; set; }
        
        private Dictionary<string, List<string>> _tokens;

        public Dictionary<string, List<String>> Tokens => _tokens;

        public DeviceTokens()
        {
            _tokens = new Dictionary<string, List<string>>();
        }
        
        public void init(string userid)
        {
            _tokens.Add(userid, new List<string>());
        }

        public void addToken(string userid, string devicetoken)
        {
            checkToken(devicetoken);
            if (_tokens.ContainsKey(userid))
            {
                _tokens[userid].Add(devicetoken);
            }
            else
            {
                var tokens = new List<string> {devicetoken};
                _tokens.Add(userid, tokens);
            }
        }
        
        public List<string> getAllTokens()
        {
            var tokens = new List<string>();
            foreach (var id in _tokens.Keys)
            {
                tokens.AddRange(_tokens[id]);
            }

            return tokens;
        }

        public List<string> getTokensByIds(List<string> userids)
        {
            var tokens = new List<string>();
            foreach (var id in userids)
            {
                tokens.AddRange(_tokens[id]);
            }

            return tokens;
        }

        private void checkToken(string devicetoken)
        {
            foreach (var tokenstore in _tokens)
            {
                if (tokenstore.Value.Contains(devicetoken))
                {
                    tokenstore.Value.Remove(devicetoken);
                }
            }
        }

        private bool checkUser(string userid)
        {
            return userid.Equals(_tokens.Keys.FirstOrDefault(u => u.Equals(userid)));
        }
    }
}